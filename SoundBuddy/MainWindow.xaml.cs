using SoundBuddy.Models;
using SoundBuddy.Services;
using SoundBuddy.ViewModels;
using SoundBuddy.Views;
using System.Windows;
using System.Windows.Controls;

namespace SoundBuddy
{
    public partial class MainWindow : Window
    {
        private readonly AudioPlayer _audioPlayer;
        private readonly DisplayController _displayController;
        private readonly PageController _pageController;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            _audioPlayer = new AudioPlayer(LbCurrentTime, LbLeftTime, progressBar);
            _displayController = new DisplayController(this);
            _pageController = new PageController(this);
        }

        private void BtnAddSongs_OnClick(object sender, RoutedEventArgs e)
        {
            _pageController.SwitchToLocalFilesPage();

            var paths = FileController.SelectSongFiles();

            if (paths == null) 
                return;

            _pageController.AddSongsToList(paths);
        }

        public void PlaySong(Song song)
        {
            _audioPlayer.Load(song.Path);
            _audioPlayer.Play();

            _displayController.ShowCurrentSongData(song);
        }

        private void BtnStop_OnClick(object sender, RoutedEventArgs e)
        {
            _audioPlayer.Stop();
        }

        private void BtnPlay_OnClick(object sender, RoutedEventArgs e)
        {
            _audioPlayer.Play();
        }

        private void BtnPause_OnClick(object sender, RoutedEventArgs e)
        {
            _audioPlayer.Pause();
        }

        private void BtnMinimize_OnClick(object sender, RoutedEventArgs e)
        {
            _displayController.MinimizeWindow();
        }

        private void BtnMaximize_OnClick(object sender, RoutedEventArgs e)
        {
            _displayController.MaximizeWindow();
        }

        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            _displayController.CloseWindow();
        }

        private void BtnPlaylists_OnClick(object sender, RoutedEventArgs e)
        {
            _pageController.SwitchToPlaylistListPage();
        }

        private void BtnLocalFiles_OnClick(object sender, RoutedEventArgs e)
        {
            _pageController.SwitchToLocalFilesPage();
        }

        private void BtnSettings_OnClick(object sender, RoutedEventArgs e)
        {
            _pageController.SwitchToSettingsPage();
        }
    }
}