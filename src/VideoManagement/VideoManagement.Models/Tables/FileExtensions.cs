using System;
using System.Collections.Generic;
using System.Text;

namespace VideoManagement.Models.Tables
{
    public class FileExtensions : BaseModel
    {
        public string Format { get; set; }


        public static List<FileExtensions> DefaultExtensions()
        {
            var defaultExtensions = new List<string>()
            {
                ".avi",".mp4",".mp3"
            };

            var fileExtensions = new List<FileExtensions>();
            foreach (var extension in defaultExtensions)
            {
                var fileExtension = new FileExtensions()
                {
                    ID = Guid.NewGuid(),
                    Format = extension
                };
                fileExtensions.Add(fileExtension);
            }

            return fileExtensions;
        }
    }
}
