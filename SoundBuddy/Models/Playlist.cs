using System.Collections.ObjectModel;

namespace SoundBuddy.Models
{
    internal class Playlist
    {
        private int _id;
        private string _name;
        private TagLib.IPicture _cover;

        public ObservableCollection<Song> songs;

        public Playlist(int id, string name, TagLib.IPicture cover)
        {
            _id = id;
            _name = name;
            _cover = cover;
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
