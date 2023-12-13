using System.Windows.Controls;
using SoundBuddy.Models;
using SoundBuddy.Views;

namespace SoundBuddy.ViewModels
{
    public class PageController
    {
        private readonly MainWindow _window;

        private readonly LocalFilesPage _localFilesPage;
        private readonly PlaylistListPage _settingsPage;
        private readonly SelectedPlaylistPage _selectedPlaylistPage;
        private readonly PlaylistListPage _playlistListPage;

        private Page _currentPage;

        public PageController(MainWindow window)
        {
            _window = window;
            _localFilesPage = new LocalFilesPage(this);
            _settingsPage = new PlaylistListPage(this);
            _selectedPlaylistPage = new SelectedPlaylistPage(this);
            _playlistListPage = new PlaylistListPage(this);

            ChangePageInFrame(_localFilesPage);
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

        public void PlaySong(Song song)
        {
            _window.PlaySong(song);
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
            ChangePageInFrame(_selectedPlaylistPage);
        }

        public void SwitchToSettingsPage()
        {
            ChangePageInFrame(_settingsPage);
        }

        private void ChangePageInFrame(Page page)
        {
            _currentPage = page;
            _window.mainFrame.Navigate(page);
        }
    }
}
