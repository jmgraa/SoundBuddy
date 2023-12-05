using SoundBuddy.Models;
using SoundBuddy.Services;
using System.Collections.ObjectModel;

namespace SoundBuddy.ViewModels
{
    internal static class SongManagement
    {
        public static ObservableCollection<Song> GetAllSongs()
        {
            var pathsAndIds = DbHelper.GetAllPathsAndIds();
            var songs = new ObservableCollection<Song>();

            foreach (var pathAndId in pathsAndIds)
                songs.Add(TagService.GetSongData(pathAndId.Item2, pathAndId.Item1));

            return songs;
        }

        public static Song? AddSongToDatabase(string path)
        {
            var id = DbHelper.AddSongToDatabase(path);

            return id != null ? TagService.GetSongData(path, (int)id) : null;
        }
    }
}
