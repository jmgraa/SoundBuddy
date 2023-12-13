using SoundBuddy.Models;
using SoundBuddy.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace SoundBuddy.Views
{
    public partial class LocalFilesPage : Page
    {
        public ObservableCollection<Song> AllSongs = SongManagement.GetAllSongs();
        private readonly PageController _pageController;

        public LocalFilesPage(PageController pageController)
        {
            InitializeComponent();

            songListView.ItemsSource = AllSongs;
            _pageController = pageController;
        }
        

        private void SongListView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (songListView.SelectedItem == null)
                return;

            _pageController.PlaySong((Song)songListView.SelectedItem);
        }
    }
}
