using System.Windows.Media.Imaging;

namespace SoundBuddy.Models
{
    public class Song(
        int id,
        string? title,
        string[] artists,
        string? album,
        string? genre,
        uint year,
        BitmapImage? cover,
        string path)
    {
        public readonly int Id = id;
        public readonly BitmapImage? Cover = cover;
        public readonly string Path = path;

        public string Title => title ?? "Unknown title";

        public string Artists => artists.Length > 0 ? string.Join(", ", artists) : "Unknown artist";

        public string Album => album ?? "Unknown album";

        public string Genre => genre ?? "Unknown genre";

        public string Year => year != 0 ? year.ToString() : "Unknown year";
    }
}
