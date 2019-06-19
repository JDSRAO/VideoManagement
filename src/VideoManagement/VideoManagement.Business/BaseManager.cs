using System;
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
        }

        public async Task PreConditions()
        {
            if (!DbExists())
            {
                await Setup();
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

        public async Task<string[]> GetAllLocalFiles()
        {
            return await Task.Run(() => 
            {
                List<string> files = new List<string>();
                var supportedExtensions = appMgmtService.GetSupportedFileExtensions();
                foreach (var extension in supportedExtensions)
                {
                    string[] localFiles = Directory.GetFiles(DirectoryPath, $"*{extension.Format}*", SearchOption.AllDirectories);
                    files.AddRange(localFiles);
                }

                return files.ToArray();
            });
        }

        public async Task Setup()
        {
            var files = await GetAllLocalFiles();
            var videos = new List<AppFile>();
            foreach (var file in files)
            {
                AppFile video = await CreateMediaFileWithCategory(file);
                videos.Add(video);
            }

            context.Files.AddRange(videos);
            var setting = context.Settings.Where(x => x.Key == "DbExists").FirstOrDefault();
            setting.Value = "true";
            context.Settings.Update(setting);
            await context.SaveChangesAsync();
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

        public async Task Refresh()
        {
            await Task.Run(async () =>
            {
                var localFilesNames = await GetAllLocalFiles();

                var dbMediaFiles = GetAllMediaFiles().ToList();

                foreach (var mediaFile in dbMediaFiles)
                {
                    if (localFilesNames.Count(x => x.Equals(mediaFile.Path)) == 0)
                    {
                        var categories = mediaFile.Categories;
                        foreach (var category in categories)
                        {
                            context.Categories.Remove(category);
                        }
                        var tags = mediaFile.Tags;
                        foreach (var tag in tags)
                        {
                            context.Tags.Remove(tag);
                        }
                        var artists = mediaFile.Artists;
                        foreach (var artist in artists)
                        {
                            context.Artists.Remove(artist);
                        }
                        context.Files.Remove(mediaFile);
                    }
                }

                foreach (var fileName in localFilesNames)
                {
                    var dbItem = context.Files.Where(x => x.Name.Equals(fileName)).FirstOrDefault();
                    if(dbItem == null)
                    {
                        var media = await CreateMediaFileWithCategory(fileName);
                        context.Files.Add(media);
                    }
                }

                await context.SaveChangesAsync();
            });
        }

        public IQueryable<AppFile> GetAllMediaFiles()
        {
            return context.Files.Select(x => new AppFile
            {
                ID = x.ID,
                Name = x.Name,
                Path = x.Path,
                CreatedOn = x.CreatedOn,
                LastAccessTime = x.LastAccessTime,
                Views = x.Views,
                Favourite = x.Favourite,
                Type = x.Type,
                Size = x.Size,
                Duration = x.Duration,
                Categories = context.Categories.Where(a => a.AppFileId == x.ID).ToList(),
                Artists = context.Artists.Where(a => a.AppFileId == x.ID).ToList(),
                Tags = context.Tags.Where(a => a.AppFileId == x.ID).ToList(),
            });
        }

        private async Task<AppFile> CreateMediaFileDetails(string file)
        {
            var fileInfo = new FileInfo(file);
            var video = new AppFile()
            {
                Name = fileInfo.Name,
                Path = file,
                CreatedOn = fileInfo.CreationTime,
                Type = appMgmtService.GetFileType(fileInfo.Name),
                Duration = await GetMediaDuration(file),
                Size = ((fileInfo.Length / 1000) / 1000)
            };
            return video;
        }

        private async Task<AppFile> CreateMediaFileWithCategory(string file)
        {
            var video = await CreateMediaFileDetails(file);
            var category = new Category()
            {
                Name = video.GetDefaultCategory()
            };
            video.Categories.Add(category);
            return video;
        }
    }
}
