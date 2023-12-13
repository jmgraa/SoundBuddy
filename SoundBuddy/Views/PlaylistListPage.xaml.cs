using SoundBuddy.ViewModels;
using SoundBuddy.Views.UserControls;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace SoundBuddy.Views
{
    public partial class PlaylistListPage : Page
    {
        private readonly PageController _pageController;
        public ObservableCollection<PlaylistUserControl> AllPlaylists = SongManagement.getUserControls();

        public PlaylistListPage(PageController pageController)
        {
            InitializeComponent();

            _pageController = pageController;

            PlaylistList.ItemsSource = AllPlaylists;
        }
    }
}
