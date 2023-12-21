using SoundBuddy.Models;
using SoundBuddy.ViewModels;
using System.Windows;

namespace SoundBuddy
{
    public partial class MainWindow
    {
        internal readonly SoundyFacade SoundyFacade;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
            SoundyFacade = new SoundyFacade(this);
        }

        private void BtnAddSongs_OnClick(object sender, RoutedEventArgs e)
        {
            SoundyFacade.SwitchToLocalFilesPage();

            var paths = SoundyFacade.SelectSongFiles();

            if (paths == null) 
                return;

            SoundyFacade.AddSongsToList(paths);
        }

        public void PlaySong(Song song)
        {
            SoundyFacade.PlaySong(song);
        }

        private void BtnStop_OnClick(object sender, RoutedEventArgs e)
        {
            SoundyFacade.Stop();
        }

        private void BtnPlay_OnClick(object sender, RoutedEventArgs e)
        {
            SoundyFacade.Play();
        }

        private void BtnPause_OnClick(object sender, RoutedEventArgs e)
        {
            SoundyFacade.Pause();
        }

        private void BtnMinimize_OnClick(object sender, RoutedEventArgs e)
        {
            SoundyFacade.MinimizeWindow();
        }

        private void BtnMaximize_OnClick(object sender, RoutedEventArgs e)
        {
            SoundyFacade.MaximizeWindow();
        }

        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            SoundyFacade.CloseWindow();
        }

        private void BtnPlaylists_OnClick(object sender, RoutedEventArgs e)
        {
            SoundyFacade.SwitchToPlaylistListPage();
        }

        private void BtnLocalFiles_OnClick(object sender, RoutedEventArgs e)
        {
            SoundyFacade.SwitchToLocalFilesPage();
        }

        private void BtnSettings_OnClick(object sender, RoutedEventArgs e)
        {
            SoundyFacade.SwitchToSettingsPage();
        }

        private void BtnCreatePlaylist_OnClick(object sender, RoutedEventArgs e)
        {
            SoundyFacade.SwitchToPlaylistListPage();
        }

        private void BtnMute_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void BtnPrevious_OnClick(object sender, RoutedEventArgs e)
        {
            SoundyFacade.Restart();
        }

        private void BtnNext_OnClick(object sender, RoutedEventArgs e)
        {
            SoundyFacade.PlaySong(SoundyFacade.GetNextSongToPlay());
        }

        private void BtnRandom_OnClick(object sender, RoutedEventArgs e)
        {
            SoundyFacade.ChangeRandomMode();
        }
    }
}