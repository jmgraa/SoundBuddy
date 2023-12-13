using System.Windows.Controls;
using SoundBuddy.Models;

namespace SoundBuddy.Views.UserControls
{
    public partial class PlaylistUserControl : UserControl
    {
        private Playlist _playlist;
        public PlaylistUserControl(Playlist playlist)
        {
            InitializeComponent();

            _playlist = playlist;

            LbName.Content = playlist.Name;
            LbDescription.Content = playlist.Description;
            //ImgCover = playlist.Cover;
        }
    }
}
