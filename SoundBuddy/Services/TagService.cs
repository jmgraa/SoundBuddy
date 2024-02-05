using SoundBuddy.Models;
using System.IO;
using System.Windows.Media.Imaging;

namespace SoundBuddy.Services
{
    internal static class TagService
    {
        public static Song? GetSongData(string path, int id)
        {
            try
            {
                using var file = TagLib.File.Create(path);

                var title = file.Tag.Title;
                var artist = file.Tag.Performers;
                var album = file.Tag.Album;
                var genres = file.Tag.Genres;
                var year = file.Tag.Year;
                BitmapImage? picture = null;

                if (file.Tag.Pictures.Length > 0)
                {
                    picture = new BitmapImage();
                    picture.BeginInit();
                    picture.StreamSource = new MemoryStream(file.Tag.Pictures[0].Data.Data);
                    picture.EndInit();
                }

                var genre = genres.Length > 0 ? genres[0] : null;

                return new Song(id, title, artist, album, genre, year, picture, path);
            }
            catch
            {
                DatabaseManagement.DeleteSongFromDatabase(id);
            }

            return null;
        }
    }
}
