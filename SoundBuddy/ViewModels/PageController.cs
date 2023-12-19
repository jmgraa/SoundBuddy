using System.Collections.ObjectModel;
using System.Windows.Controls;
using SoundBuddy.Interfaces;
using SoundBuddy.Models;
using SoundBuddy.Views;
using SoundBuddy.Views.UserControls;

namespace SoundBuddy.ViewModels
{
    public class PageController
    {
        private readonly MainWindow _window;

        private readonly LocalFilesPage _localFilesPage;
        private readonly SettingsPage _settingsPage;
        private SelectedPlaylistPage? _selectedPlaylistPage;
        private readonly PlaylistListPage _playlistListPage;

        public PageController(MainWindow window)
        {
            _window = window;

            _localFilesPage = new LocalFilesPage(_window);
            _settingsPage = new SettingsPage();
            _selectedPlaylistPage = null;
            _playlistListPage = new PlaylistListPage(_window, getUserControls());

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
        }

        public ObservableCollection<PlaylistUserControl> getUserControls()
        {
            var controls = new ObservableCollection<PlaylistUserControl>();

            foreach (var playlist in SongManagement.GetAllPlaylists())
            {
                controls.Add(new PlaylistUserControl(playlist, _window));
            }

            return controls;
        }

        public void PlaySong(Song song)
        {
            _window.PlaySong(song);
        }

        public void SwitchToLocalFilesPage()
        {
            _localFilesPage.RefreshContent();
            ChangePageInFrame(_localFilesPage);
        }

        public void SwitchToPlaylistListPage()
        {
            ChangePageInFrame(_playlistListPage);
        }

        public void SwitchToSelectedPlaylistPage()
        {
            ChangePageInFrame(_selectedPlaylistPage);
        }

        public void SwitchToSettingsPage()
        {
            ChangePageInFrame(_settingsPage);
        }

        private void ChangePageInFrame(Page page)
        {
            if (page != _selectedPlaylistPage)
                _selectedPlaylistPage = null;

            _window.mainFrame.Navigate(page);
        }
    }
}
