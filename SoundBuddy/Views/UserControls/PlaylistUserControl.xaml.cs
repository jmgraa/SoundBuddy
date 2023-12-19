using System.Windows.Controls;
using System.Windows.Input;
using SoundBuddy.Models;
using SoundBuddy.ViewModels;

namespace SoundBuddy.Views.UserControls
{
    public partial class PlaylistUserControl
    {
        private readonly MainWindow _window;

        private Playlist _playlist;

        public PlaylistUserControl(Playlist playlist, MainWindow window)
        {
            InitializeComponent();

            _playlist = playlist;
            _window = window;

            LbName.Content = playlist.Name;
            LbDescription.Content = playlist.Description;
            ImgCover.Source = playlist.Cover;
        }

        private void PlaylistUserControl_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _window.SoundyFacade.LoadSelectedPlaylistPage(_playlist);
        }
    }
}
