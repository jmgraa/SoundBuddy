using SoundBuddy.Models;
using SoundBuddy.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SoundBuddy.Interfaces;

namespace SoundBuddy.Views
{
    public partial class LocalFilesPage : IRefreshable
    {
        private readonly MainWindow _window;

        public ObservableCollection<Song> AllSongs = SongManagement.GetAllSongs();

        public LocalFilesPage(MainWindow window)
        {
            InitializeComponent();

            _window = window;

            SongListViewUrCl.songListView.ItemsSource = AllSongs;
        }
        
        private void SongListView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SongListViewUrCl.songListView.SelectedItem == null)
                return;

            _window.SoundyFacade.ChangeCurrentQueue(AllSongs);
            _window.SoundyFacade.PlaySong((Song)SongListViewUrCl.songListView.SelectedItem);
        }

        public void RefreshContent()
        {
            AllSongs = SongManagement.GetAllSongs();
        }
    }
}
