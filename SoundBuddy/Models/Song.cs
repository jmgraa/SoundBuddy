using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SoundBuddy.Models
{
    public class Song : INotifyPropertyChanged
    {
        private int _id;
        private string? _title;
        private string? _artist;
        private string? _album;
        private string? _genre;
        private uint? _year;
        private TagLib.IPicture? _cover;
        private string _path;

        public event PropertyChangedEventHandler PropertyChanged;

        public Song(int id, string? title, string? artist, string? album, string? genre, uint? year, TagLib.IPicture? cover, string path)
        {
            _id = id;
            _title = title;
            _artist = artist;
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

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public string Artist
        {
            get => _artist;
            set
            {
                _artist = value;
                OnPropertyChanged();
            }
        }

        public string Album
        {
            get => _album;
            set
            {
                _album = value;
                OnPropertyChanged();
            }
        }

        public string Genre
        {
            get => _genre;
            set
            {
                _genre = value;
                OnPropertyChanged();
            }
        }

        public uint? Year
        {
            get => _year;
            set
            {
                _year = value;
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
