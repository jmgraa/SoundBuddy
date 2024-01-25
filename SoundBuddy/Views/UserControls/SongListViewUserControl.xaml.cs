using System.Windows;
using System.Windows.Controls;
using SoundBuddy.Models;
using SoundBuddy.Services;
using SoundBuddy.ViewModels;

namespace SoundBuddy.Views.UserControls
{
    public partial class SongListViewUserControl
    {
        public SongListViewUserControl()
        {
            InitializeComponent();
        }

        private void MenuAddToPlaylist_OnClick(object sender, RoutedEventArgs e)
        {
            if (songListView.SelectedItem == null) return;

            var song = (Song)songListView.SelectedItem;

            var wind = new PlaylistSelection(song);
            wind.Show();
        }

        private void MenuDeleteSong_OnClick(object sender, RoutedEventArgs e)
        {
	        if (songListView.SelectedItem == null) return;

	        var song = (Song)songListView.SelectedItem;

            DbHelper.DeleteSongFromDatabase(song.Id);

            var parentWindow = Window.GetWindow(this);

            if (parentWindow is MainWindow mainWindow)
            {
	            mainWindow.SoundyFacade.UpdatePlaylists();
            }
        }
    }
}
