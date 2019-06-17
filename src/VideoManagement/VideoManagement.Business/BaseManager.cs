﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoManagement.DataAccess.SQLite;
using VideoManagement.Models.Tables;

namespace VideoManagement.Business
{
    public class BaseManager
    {
        private string DirectoryPath { get; }
        public FileManagerDbContext context { get; }
        private AppMgmtService appMgmtService { get; }

        private string DBFileName = "searchEngine";

        public BaseManager(string path)
        {
            DirectoryPath = path;
            appMgmtService = new AppMgmtService();
            Guid pathId = appMgmtService.ConfigurePreConditionsForPath(path);
            var fileName = $"{pathId}_{DBFileName}.db";
            var connectionString = Path.Combine(Directory.GetCurrentDirectory(), "db", fileName);
            context = new FileManagerDbContext(connectionString);
            if (!DbExists())
            {
                Task.Run(async () => 
                {
                    await Setup();
                });
            }
        }

        public bool DbExists()
        {
            var setting = context.Settings.Where(x => x.Key.Equals("DbExists")).FirstOrDefault();
            if (setting != null && setting.Value == "false")
            {
                return false;
            }
            return true;
        }

        public string[] GetAllLocalFiles()
        {
            List<string> files = new List<string>();
            var supportedExtensions = appMgmtService.GetSupportedFileExtensions();
            foreach (var extension in supportedExtensions)
            {
                string[] localFiles = Directory.GetFiles(DirectoryPath, $"*{extension.Format}*", SearchOption.AllDirectories);
                files.AddRange(localFiles);
            }
            
            return files.ToArray();
        }

        private async Task Setup()
        {
            var files = GetAllLocalFiles();
            var videos = new List<AppFile>();
            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                var video = new AppFile()
                {
                    Name = fileInfo.Name,
                    Path = file,
                    CreatedOn = fileInfo.CreationTime,
                    Type = appMgmtService.GetFileType(fileInfo.Name),
                    Duration = await GetMediaDuration(file),
                    Size = ( (fileInfo.Length / 1000) / 1000)
                };
                var category = new Category()
                {
                    Name = video.GetDefaultCategory()
                };
                video.Categories.Add(category);
                videos.Add(video);
            }

            context.Files.AddRange(videos);
            var setting = context.Settings.Where(x => x.Key == "DbExists").FirstOrDefault();
            setting.Value = "true";
            context.Settings.Update(setting);
            context.SaveChanges();
        }

        public async Task<TimeSpan> GetMediaDuration(string filePath)
        {
            return await Task.Run(() =>
            {
                var inputFile = new MediaToolkit.Model.MediaFile { Filename = @filePath };
                using (var engine = new MediaToolkit.Engine())
                {
                    engine.GetMetadata(inputFile);
                }

                return inputFile.Metadata.Duration;
            });
            
        }
    }
}
