using SoundBuddy.Models;
using SoundBuddy.Views;
using SoundBuddy.Views.UserControls;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace SoundBuddy.ViewModels
{
    public class PageController
    {
        private readonly MainWindow _window;

        private readonly LocalFilesPage _localFilesPage;
        private readonly PlaylistListPage _playlistListPage;

        private SelectedPlaylistPage? _selectedPlaylistPage;

        public PageController(MainWindow window)
        {
            _window = window;

            _localFilesPage = new LocalFilesPage(_window);
            _playlistListPage = new PlaylistListPage(_window, GetUserControls());
            _selectedPlaylistPage = null;

            ChangePageInFrame(_localFilesPage);
        }

        public void LoadSelectedPlaylistPage(Playlist playlist)
        {
            _selectedPlaylistPage = new SelectedPlaylistPage(_window, playlist);
            SwitchToSelectedPlaylistPage();
        }

        // Adding song to local files list
        public void AddSongsToList(string[] paths)
        {
            foreach (var path in paths)
            {
                var newSong = SongManagement.AddSongToDatabase(path);
                
                if (newSong != null)
                    _localFilesPage.AllSongs.Add(newSong);
            }

            SwitchToLocalFilesPage();
        }

        public ObservableCollection<PlaylistUserControl> GetUserControls()
        {
            var controls = new ObservableCollection<PlaylistUserControl>();

            foreach (var playlist in SongManagement.GetAllPlaylists())
            {
                controls.Add(new PlaylistUserControl(playlist, _window));
            }

            return controls;
        }

        public void SwitchToLocalFilesPage()
        {
            ChangePageInFrame(_localFilesPage);
        }

        public void SwitchToPlaylistListPage()
        {
            ChangePageInFrame(_playlistListPage);
        }

        public void SwitchToSelectedPlaylistPage()
        {
            if (_selectedPlaylistPage != null)
                ChangePageInFrame(_selectedPlaylistPage);
        }

        public void RefreshPlaylistPage()
        {
            _playlistListPage.RefreshContent();
        }

        private void ChangePageInFrame(Page page)
        {
            if (page != _selectedPlaylistPage)
                _selectedPlaylistPage = null;

            _window.mainFrame.Navigate(page);
        }
    }
}
