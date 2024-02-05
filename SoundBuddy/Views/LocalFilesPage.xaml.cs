using SoundBuddy.Models;
using SoundBuddy.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SoundBuddy.Views
{
	public partial class LocalFilesPage
    {
        private readonly MainWindow _window;

        public ObservableCollection<Song> AllSongs = SongManagement.GetAllSongs();

        public LocalFilesPage(MainWindow window)
        {
            InitializeComponent();

            _window = window;

            SongListViewUrCl.SongListView.ItemsSource = AllSongs;
        }
        
        private void OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SongListViewUrCl.SongListView.SelectedItem == null)
                return;

            _window.SoundyFacade.PlaySong((Song)SongListViewUrCl.SongListView.SelectedItem);
            _window.SoundyFacade.ChangeCurrentQueue(AllSongs);
        }
    }
}
