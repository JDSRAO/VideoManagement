using System;
using System.Collections.Generic;
using System.Text;

namespace VideoManagement.Models.Tables
{
    public class FileExtensions : BaseModel
    {
        public string Format { get; set; }
        public FileType Type { get; set; }


        public static List<FileExtensions> DefaultExtensions()
        {
            var defaultExtensions = new Dictionary<string, FileType>();
            defaultExtensions.Add(".avi", FileType.Video);
            defaultExtensions.Add(".mp4", FileType.Video);
            defaultExtensions.Add(".mp3", FileType.Audio);

            var fileExtensions = new List<FileExtensions>();
            foreach (var extension in defaultExtensions)
            {
                var fileExtension = new FileExtensions()
                {
                    ID = Guid.NewGuid(),
                    Format = extension.Key,
                    Type = extension.Value
                };
                fileExtensions.Add(fileExtension);
            }

            return fileExtensions;
        }
    }

    public enum FileType
    {
        Audio,
        Video
    }
}
