using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using VideoManagement.Models;
using Newtonsoft.Json;

namespace VideoManagement.DataAccess.FileSystem
{
    public class FileRepository<T> : IVideoMgmtRepository<T> where T : class
    {
        private string DBFileName = "videoManagement.json";
        private string PathWithFileName { get;}
        private string DirectoryPath { get;}

        public FileRepository(string path)
        {
            DirectoryPath = path;
            PathWithFileName = Path.Combine(path, DBFileName);
            if(File.Exists(PathWithFileName))
            {
                Refresh();
            }
            else
            {
                File.Create(PathWithFileName);
            }
        }

        public void Add(T[] video)
        {
            throw new NotImplementedException();
        }

        public void Add(T video)
        {
            throw new NotImplementedException();
        }

        public void Update(T video)
        {
            throw new NotImplementedException();
        }

        public void Delete(T video)
        {
            throw new NotImplementedException();
        }

        public void Refresh()
        {
            var localFiles = GetAllLocalVideoFiles();
            var scannedFiles = GetScannedFiles();
        }

        public string[] GetAllLocalVideoFiles()
        {
            string[] files = Directory.GetFiles(DirectoryPath, "*.*", SearchOption.AllDirectories);
            return files;
        }

        private List<Video> GetScannedFiles()
        {
            var content = File.ReadAllText(PathWithFileName);
            return JsonConvert.DeserializeObject<List<Video>>(content);
        }

        private void SaveToFile()
        {

        }
    }
}
