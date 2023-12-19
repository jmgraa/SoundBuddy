using SoundBuddy.Models;
using SoundBuddy.Services;
using System.Collections.ObjectModel;

namespace SoundBuddy.ViewModels
{
    internal class SongManagement
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

        public static ObservableCollection<Playlist> GetAllPlaylists()
        {
            var playlists = new ObservableCollection<Playlist>();

            var playlistData = DbHelper.GetAllPlaylists();

            foreach (var data in playlistData)
            {
                var songsOnPlaylist = GetSongsOnPlaylist(data.Item1);
                // TODO fix image cast
                playlists.Add(new Playlist(data.Item1, data.Item2, data.Item3, data.Item4, songsOnPlaylist));
            }

            return playlists;
        }

        public static ObservableCollection<Song> GetSongsOnPlaylist(int id)
        {
            var allSongs = GetAllSongs();
            var songsOnPlaylist = new ObservableCollection<Song>();
            var songsOnPlaylistIds = DbHelper.GetSongsOnPlaylistIds(id);

            foreach (var song in allSongs)
            {
                if (songsOnPlaylistIds.Contains(song.Id))
                    songsOnPlaylist.Add(song);
            }

            return songsOnPlaylist;
        }
    }
}
