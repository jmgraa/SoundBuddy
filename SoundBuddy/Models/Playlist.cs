using System.Collections.ObjectModel;
using SoundBuddy.ViewModels;

namespace SoundBuddy.Models
{
    public class Playlist
    {
        private int _id;
        private string _name;
        private string? _description;
        private TagLib.IPicture? _cover;

        private ObservableCollection<Song> _songs;

        public Playlist(int id, string name, string? description, TagLib.IPicture? cover, ObservableCollection<Song> songs)
        {
            _id = id;
            _name = name;
            _description = description;
            _cover = cover;

            _songs = songs;
        }

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
            }
        }
        public TagLib.IPicture Cover
        {
            get => _cover;
            set
            {
                _cover = value;
            }
        }
    }
}
