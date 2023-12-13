using SoundBuddy.Models;
using SoundBuddy.Services;
using SoundBuddy.Views.UserControls;
using System.Collections.ObjectModel;

namespace SoundBuddy.ViewModels
{
    internal static class SongManagement
    {
        private static ObservableCollection<Song> _allSongs = GetAllSongs();
        private static ObservableCollection<Playlist> _allPlaylists = GetAllPlaylists();
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

        public static ObservableCollection<PlaylistUserControl> getUserControls()
        {
            var controls = new ObservableCollection<PlaylistUserControl>();

            foreach (var playlist in _allPlaylists)
            {
                controls.Add(new PlaylistUserControl(playlist));
            }

            return controls;
        }

        public static ObservableCollection<Playlist> GetAllPlaylists()
        {
            var playlists = new ObservableCollection<Playlist>();

            var playlistData = DbHelper.GetAllPlaylists();

            foreach (var data in playlistData)
            {
                var songsOnPlaylist = GetSongsOnPlaylist(data.Item1);
                // TODO fix image cast
                playlists.Add(new Playlist(data.Item1, data.Item2, data.Item3, null, songsOnPlaylist));
            }

            return playlists;
        }

        public static ObservableCollection<Song> GetSongsOnPlaylist(int id)
        {
            var songsOnPlaylist = new ObservableCollection<Song>();
            var songsOnPlaylistIds = DbHelper.GetSongsOnPlaylistIds(id);

            foreach (var song in _allSongs)
            {
                if (songsOnPlaylistIds.Contains(song.Id))
                    songsOnPlaylist.Add(song);
            }

            return songsOnPlaylist;
        }
    }
}
