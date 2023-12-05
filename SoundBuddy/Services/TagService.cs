using SoundBuddy.Models;

namespace SoundBuddy.Services
{
    internal static class TagService
    {
        public static Song GetSongData(string path, int id)
        {
            using var file = TagLib.File.Create(path);

            var title = file.Tag.Title;
            var artist = file.Tag.FirstAlbumArtist;
            var album = file.Tag.Album;
            var genres = file.Tag.Genres;
            var year = file.Tag.Year;

            TagLib.IPicture? picture = null;

            if (file.Tag.Pictures.Length > 0)
                picture = file.Tag.Pictures[0];

            var genre = genres.Length > 0 ? genres[0] : null;

            return new Song(id, title, artist, album, genre, year, picture, path);
        }
    }
}
