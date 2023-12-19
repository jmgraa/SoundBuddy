using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace SoundBuddy.Models
{
    public class Playlist(
        int id,
        string name,
        string? description,
        BitmapImage? cover,
        ObservableCollection<Song> songs)
    {
        public readonly int Id = id;
        public string Name = name;
        public string? Description = description;
        public BitmapImage? Cover = cover;
        public ObservableCollection<Song> Songs = songs;
    }
}
