using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoundBuddy.Models;
using SoundBuddy.Services;

namespace SoundBuddy.ViewModels
{
    internal static class SongManagement
    {
        public static ObservableCollection<Song> GetAllSongs()
        {
            List<(int, String)> pathsAndIds = DbHelper.GetAllPathsAndIds();
            ObservableCollection<Song> songs = new ObservableCollection<Song>();

            foreach (var pathAndId in pathsAndIds)
            {
                songs.Add(TagService.GetSongData(pathAndId.Item2, pathAndId.Item1));
            }

            return songs;
        }

        public static Song? AddSongToDatabase(string path)
        {
            var id = DbHelper.AddSongToDatabase(path);

            if (id != null)
                return TagService.GetSongData(path, (int)id);

            return null;
        }
    }
}
