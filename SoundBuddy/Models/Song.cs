using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SoundBuddy.Models
{
    public class Song : INotifyPropertyChanged
    {
        private readonly int _id;
        private string? _title;
        private readonly string[]? _artists;
        private string? _album;
        private string? _genre;
        private uint _year;
        private TagLib.IPicture? _cover;
        private string _path;

        public event PropertyChangedEventHandler PropertyChanged;

        public Song(int id, string? title, string[]? artists, string? album, string? genre, uint year, TagLib.IPicture? cover, string path)
        {
            _id = id;
            _title = title;
            _artists = artists;
            _album = album;
            _genre = genre;
            _year = year;
            _cover = cover;
            _path = path;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int Id => _id;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public string? Artists
        {
            get => _artists != null ? string.Join(", ", _artists) : "Unknown artist";
            set
            {
                // TODO type
                OnPropertyChanged();
            }
        }

        public string Album
        {
            get => _album ?? "Unknown album";
            set
            {
                _album = value;
                OnPropertyChanged();
            }
        }

        public string Genre
        {
            get => _genre ?? "Unknown genre";
            set
            {
                _genre = value;
                OnPropertyChanged();
            }
        }

        public string Year
        {
            get => _year != 0 ? _year.ToString() : "Unknown year";
            set
            {
                _year = uint.Parse(value);
                OnPropertyChanged();
            }
        }

        public TagLib.IPicture? Cover
        {
            get => _cover;
            set
            {
                _cover = value;
                OnPropertyChanged();
            }
        }

        public string Path
        {
            get => _path;
            set
            {
                _path = value;
                OnPropertyChanged();
            }
        }

        
    }
}
