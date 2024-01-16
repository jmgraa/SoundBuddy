using SoundBuddy.Interfaces;
using SoundBuddy.ViewModels;
using SoundBuddy.Views.UserControls;
using System.Collections.ObjectModel;

namespace SoundBuddy.Views
{
    public partial class PlaylistListPage : IRefreshable
    {
        private readonly MainWindow _window;

        public ObservableCollection<PlaylistUserControl> AllPlaylists;

        public PlaylistListPage(MainWindow window, ObservableCollection<PlaylistUserControl> allPlaylist)
        {
            InitializeComponent();

            _window = window;

            AllPlaylists = allPlaylist;
            PlaylistList.ItemsSource = AllPlaylists;
        }

        public void RefreshContent()
        {
            AllPlaylists = _window.SoundyFacade.GetUserControls();
            PlaylistList.ItemsSource = AllPlaylists;
        }
    }
}
