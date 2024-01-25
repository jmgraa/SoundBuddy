using SoundBuddy.Models;
using SoundBuddy.Services;
using SoundBuddy.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SoundBuddy.Views
{
	public partial class PlaylistSelection
    {
        private ObservableCollection<Playlist> DataList = SongManagement.GetAllPlaylists();
        private Song _song;

        public PlaylistSelection(Song song)
        {
            InitializeComponent();

            _song = song;

            var names = new ObservableCollection<MyData>();

            foreach (var playlist in DataList)
            {
                names.Add(new MyData{Column1 = playlist.Name});
            }

            myListView.ItemsSource = names;
        }

        private void MyListView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (myListView.SelectedItem == null)
                return;

            var list = new List<Playlist>(DataList);

            var dso = (MyData)myListView.SelectedItem;

            var foundSong = list.Find(playlist => playlist.Name == dso.Column1);

            DbHelper.AddSongToPlaylist(foundSong, _song.Id);
            foundSong.Songs = SongManagement.GetSongsOnPlaylist(foundSong.Id);

            Close();
        }

        private class MyData
        {
            public string Column1 { get; set; }
        }
    }
}
