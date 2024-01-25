using SoundBuddy.ViewModels;
using System.Windows.Controls;
using SoundBuddy.Models;
using System.Windows.Input;

namespace SoundBuddy.Views
{
    public partial class SelectedPlaylistPage
    {
        private readonly MainWindow _window;

        public readonly Playlist _currentPlaylist;

        public SelectedPlaylistPage(MainWindow window, Playlist playlist)
        {
            InitializeComponent();

            _window = window;

            _currentPlaylist = playlist;
            SongListViewUrCl.songListView.ItemsSource = _currentPlaylist.Songs;

            LbName.Content = playlist.Name;
            LbDescription.Content = playlist.Description;
            ImgCover.Source = playlist.Cover;
        }

        private void SongListView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SongListViewUrCl.songListView.SelectedItem == null)
                return;

            _window.SoundyFacade.ChangeCurrentQueue(_currentPlaylist.Songs);
            _window.SoundyFacade.PlaySong((Song)SongListViewUrCl.songListView.SelectedItem);
        }
    }
}
