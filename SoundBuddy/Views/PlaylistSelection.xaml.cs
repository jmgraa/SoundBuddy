using SoundBuddy.Models;
using SoundBuddy.Services;
using SoundBuddy.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SoundBuddy.Views
{
	public partial class PlaylistSelection
    {
        private readonly ObservableCollection<Playlist> _listOfPlaylists = SongManagement.GetAllPlaylists();
        private readonly Song _song;

        public PlaylistSelection(Song song)
        {
            InitializeComponent();

            _song = song;

            var names = new ObservableCollection<ColumnPlaylist>();

            foreach (var playlist in _listOfPlaylists)
            {
                names.Add(new ColumnPlaylist{Column1 = playlist.Name});
            }

            MyListView.ItemsSource = names;
        }

        private void OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (MyListView.SelectedItem == null)
                return;

            var list = new List<Playlist>(_listOfPlaylists);

            var dso = (ColumnPlaylist)MyListView.SelectedItem;

            var foundSong = list.Find(playlist => playlist.Name == dso.Column1);

            DatabaseManagement.AddSongToPlaylist(foundSong!, _song.Id);
            foundSong!.Songs = SongManagement.GetSongsOnPlaylist(foundSong.Id);

            Close();
        }

        private class ColumnPlaylist
        {
            public string Column1 { get; init; } = null!;
        }
    }
}
