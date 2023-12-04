using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SoundBuddy.Models
{
    public class Song : INotifyPropertyChanged
    {
        private string? _title;
        private string? _artist;
        private string? _album;
        private string? _genre;
        private uint? _year;
        private TagLib.IPicture? _cover;

        public Song(string title, string artist, string album, string? genre, uint? year, TagLib.IPicture? cover)
        {
            _title = title;
            _artist = artist;
            _album = album;
            _genre = genre;
            _year = year;
            _cover = cover;
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
