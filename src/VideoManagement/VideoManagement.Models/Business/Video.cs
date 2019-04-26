using System;
using System.Collections.Generic;
using System.Text;

namespace VideoManagement.Models.Business
{
    public class Video : BaseModel
    {
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Path
        {
            get => path;
            set
            {
                path = value;
                OnPropertyChanged("Path");
            }
        }

        public List<Category> Categories
        {
            get => categories;
            set
            {
                categories = value;
                OnPropertyChanged("Categories");
            }
        }

        public List<Tag> Tags
        {
            get => tags;
            set
            {
                tags = value;
                OnPropertyChanged("Tags");
            }
        }

        public DateTime CreatedOn
        {
            get => createdOn;
            set
            {
                createdOn = value;
                OnPropertyChanged("CreatedOn");
            }
        }

        public DateTime? LastAccessTime
        {
            get => lastAccessTime;
            set
            {
                lastAccessTime = value;
                OnPropertyChanged("LastAccessTime");
            }
        }

        public long Views
        {
            get => views;
            set
            {
                views = value;
                OnPropertyChanged("Views");
            }
        }

        public List<Artist> Artists
        {
            get => artists;
            set
            {
                artists = value;
                OnPropertyChanged("Artists");
            }
        }

        public bool Favourite
        {
            get => favourite;
            set
            {
                favourite = value;
                OnPropertyChanged("Favourite");
            }
        }


        private string name { get; set; }
        private string path { get; set; }
        private List<Category> categories { get; set; }
        private List<Tag> tags { get; set; }
        private DateTime createdOn { get; set; }
        private DateTime? lastAccessTime { get; set; }
        private long views { get; set; }
        private List<Artist> artists { get; set; }
        private bool favourite { get; set; }
    }
}
