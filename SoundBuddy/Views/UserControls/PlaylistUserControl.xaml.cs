using System.Windows.Controls;
using SoundBuddy.Models;
using System.Windows.Input;

namespace SoundBuddy.Views.UserControls
{
    public partial class PlaylistUserControl
    {
        private readonly MainWindow _window;

        private readonly Playlist _playlist;

        public PlaylistUserControl(Playlist playlist, MainWindow window)
        {
            InitializeComponent();

            _playlist = playlist;
            _window = window;

            LbName.Content = playlist.Name;
            LbDescription.Content = playlist.Description;

            ImgCover.Source = playlist.Cover ?? Tools.NoImage; 
        }

        private void PlaylistUserControl_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _window.SoundyFacade.LoadSelectedPlaylistPage(_playlist);
        }

        private void FrameworkElement_OnContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            
        }
    }
}
