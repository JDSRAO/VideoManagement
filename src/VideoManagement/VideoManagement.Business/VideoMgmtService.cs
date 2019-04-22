using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VideoManagement.DataAccess;
using VideoManagement.DataAccess.FileSystem;
using VideoManagement.DataAccess.SQLite;
using VideoManagement.Models;

namespace VideoManagement.Business
{
    public class VideoMgmtService
    {
        private IVideoMgmtRepository repository;
        private string DirectoryPath { get; }
        private string FilesToConsider { get; }

        public VideoMgmtService(string path, string fileExtension)
        {
            FilesToConsider = fileExtension;
            DirectoryPath = path;
            repository = new FileManagerRepository(path, fileExtension);
        }

        public List<Video> GetAllVideo()
        {
            return repository.Get();
        }

        public Guid Add(Video video)
        {
            return repository.Add(video);
        }

        public void Update(Video video)
        {
            repository.Update(video);
        }

        public void Delete(Guid id)
        {
            repository.Delete(id);
        }

        public bool Setup()
        {
            try
            {
                var files = GetAllLocalFiles();
                var videos = new List<Video>();
                foreach (var file in files)
                {
                    var video = new Video()
                    {
                        ID = Guid.NewGuid(),
                        Path = file
                    };
                    var category = new Category()
                    {
                        ID = Guid.NewGuid(),
                        Name = video.GetDefaultCategory()
                    };
                    video.Categories.Add(category);
                    videos.Add(video);
                }

                repository.Add(videos);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }

        public List<Video> Search(string query)
        {
            return null;
        }

        public string[] GetAllLocalFiles()
        {
            string[] files = Directory.GetFiles(DirectoryPath, $"*{FilesToConsider}*", SearchOption.AllDirectories);
            return files;
        }

    }
}
