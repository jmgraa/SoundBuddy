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
            List<String> paths = DbHelper.GetAllPaths();
            ObservableCollection<Song> songs = new ObservableCollection<Song>();

            foreach (var path in paths)
            {
                songs.Append(TagService.GetSongData(path));
            }

            return songs;
        }
    }
}
