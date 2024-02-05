using SoundBuddy.Models;
using System.Windows.Input;

namespace SoundBuddy.Views
{
	public partial class SelectedPlaylistPage
    {
        private readonly MainWindow _window;
        public readonly Playlist CurrentPlaylist;

        public SelectedPlaylistPage(MainWindow window, Playlist playlist)
        {
            InitializeComponent();

            _window = window;

            CurrentPlaylist = playlist;
            SongListViewUrCl.SongListView.ItemsSource = CurrentPlaylist.Songs;

            LbName.Content = playlist.Name;
            LbDescription.Content = playlist.Description;
            ImgCover.Source = playlist.Cover;
        }

        private void OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SongListViewUrCl.SongListView.SelectedItem == null)
                return;

            _window.SoundyFacade.ChangeCurrentQueue(CurrentPlaylist.Songs);
            _window.SoundyFacade.PlaySong((Song)SongListViewUrCl.SongListView.SelectedItem);
        }
    }
}
