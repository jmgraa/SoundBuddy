using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoundBuddy.Models;

namespace SoundBuddy.Services
{
    internal static class TagService
    {
        public static Song GetSongData(string path)
        {
            using (var file = TagLib.File.Create(path))
            {
                string? title = file.Tag.Title;
                string? artist = file.Tag.FirstAlbumArtist;
                string? album = file.Tag.Album;
                string[] genres = file.Tag.Genres;
                uint? year = file.Tag.Year;

                TagLib.IPicture? picture = null;

                if (file.Tag.Pictures.Length > 0)
                {
                     picture = file.Tag.Pictures[0];
                }

                var genre = genres.Length > 0 ? genres[0] : null;

                return new Song(title, artist, album, genre, year, picture);
            }
        }
    }
}
