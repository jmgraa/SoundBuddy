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

        private LocalFilesPage _localFilesPage;
        private PlaylistListPage _playlistListPage;

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
	        playlist.Songs = SongManagement.GetSongsOnPlaylist(playlist.Id);
            _selectedPlaylistPage = new SelectedPlaylistPage(_window, playlist);
            SwitchToSelectedPlaylistPage();
        }
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

        public void UpdatePages()
        {
	        _localFilesPage = new LocalFilesPage(_window);
	        _playlistListPage = new PlaylistListPage(_window, GetUserControls());


	        if (_selectedPlaylistPage != null)
	        {
		        var playlistToCopy = _selectedPlaylistPage._currentPlaylist.Id;
		        _selectedPlaylistPage._currentPlaylist.Songs = SongManagement.GetSongsOnPlaylist(playlistToCopy);
		        var playlist = _selectedPlaylistPage._currentPlaylist;

		        var selected = new SelectedPlaylistPage(_window, playlist);
		        SwitchToPlaylistListPage();


                _selectedPlaylistPage = selected;
				SwitchToSelectedPlaylistPage();
	        }
	        else
	        {
		        SwitchToPlaylistListPage();
                SwitchToLocalFilesPage();
	        }
		        
        }
    }
}
