using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using VideoManagement.Models;
using Newtonsoft.Json;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;

namespace VideoManagement.DataAccess.FileSystem
{
    public class FileRepository : IVideoMgmtRepository
    {
        private string DBFileName = "videoManagerData.json";
        private string PathWithFileName { get;}
        private string DirectoryPath { get;}
        private string FilesToConsider { get; }

        public List<Video> Videos { get; }

        public FileRepository(string path, string fileExtension)
        {
            DirectoryPath = path;
            FilesToConsider = fileExtension;
            var fileName = $"{FilesToConsider}_{DBFileName}";
            PathWithFileName = Path.Combine(path, fileName);
            Videos = new List<Video>();
            CreateDBIfNotExistsAndAddDefaultData();
            Videos = GetScannedFiles();
        }

        public void Add(List<Video> video)
        {
            Videos.AddRange(video);
            SaveToFile();
        }

        public void Add(Video video)
        {
            Videos.Add(video);
            SaveToFile();
        }

        public void Update(Video video)
        {
            var videoToUpdate = Videos.Where(x => x.ID == video.ID).FirstOrDefault();
            var flags = BindingFlags.Public | BindingFlags.Instance;
            var videoProperties = video.GetType().GetProperties(flags);
            var videoToUpdateProperties = videoToUpdate.GetType().GetProperties(flags);
            foreach (var property in videoProperties)
            {
                if (property.GetValue(video) != null && property.CanWrite)
                {
                    var dataProperty = videoToUpdateProperties.FirstOrDefault(x => x.Name.Equals(property.Name));
                    dataProperty.SetValue(videoToUpdate, property.GetValue(video));
                }
            }

            Videos.Remove(video);
            Videos.Add(videoToUpdate);
            SaveToFile();
        }

        public void Delete(Video video)
        {
            Videos.Remove(video);
            SaveToFile();
        }

        public bool DbExists()
        {
            return File.Exists(PathWithFileName);
        }

        public Video Get(Guid id)
        {
            return Videos.Where(x => x.ID == id).FirstOrDefault();
        }

        public List<Video> Get(string query = null)
        {
            if(string.IsNullOrEmpty(query))
            {
                return Videos;
            }
            else
            {
                return Videos.Where(x => x.Name.Contains(query) || x.Categories.Contains(query) || x.Tags.Contains(query)).ToList();
            }
        }

        public List<Video> Get(Func<Video, bool> predicate)
        {
            return Videos.Where(predicate).ToList();
        }

        public void Refresh()
        {
            var scannedFiles = GetScannedFiles();
        }

        public string[] GetAllLocalFiles()
        {
            string[] files = Directory.GetFiles(DirectoryPath, $"*{FilesToConsider}*", SearchOption.AllDirectories);
            return files;
        }

        private List<Video> GetScannedFiles()
        {
            var content = File.ReadAllText(PathWithFileName);
            return JsonConvert.DeserializeObject<List<Video>>(content);
        }

        private void SaveToFile()
        {
            var content = JsonConvert.SerializeObject(Videos);
            using (var stream = File.Open(PathWithFileName, FileMode.Open))
            {
                StreamWriter streamWriter = new StreamWriter(stream);
                streamWriter.Write(content);
                streamWriter.Close();
            }
        }

        private void CreateDBIfNotExistsAndAddDefaultData()
        {
            if (!DbExists())
            {
                var files = GetAllLocalFiles();
                var videos = new List<Video>();
                foreach (var file in files)
                {
                    var video = new Video()
                    {
                        ID = Guid.NewGuid(),
                        Path = file,
                        Categories = new List<string>(),
                        Tags = new List<string>()
                    };
                    video.Categories.Add(video.GetDefaultCategory());
                    videos.Add(video);
                }
                File.WriteAllText(PathWithFileName, "");
                Add(videos);
            }
        }
    }
}
