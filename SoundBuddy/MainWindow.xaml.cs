using SoundBuddy.Models;
using SoundBuddy.Services;
using SoundBuddy.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace SoundBuddy
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Song> AllSongs = SongManagement.GetAllSongs();

        private readonly AudioPlayer _audioPlayer;
        private readonly DisplayController _displayController;
        private readonly FileController _fileController;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            songListView.ItemsSource = AllSongs;

            _audioPlayer = new AudioPlayer(LbCurrentTime, LbCurrentTime);
            _displayController = new DisplayController(this);
            _fileController = new FileController(this);
        }

        private void BtnAddSongs_OnClick(object sender, RoutedEventArgs e)
        {
            var paths = _fileController.SelectSongFiles();

            if (paths == null) 
                return;

            foreach (var path in paths)
            {
                var newSong = SongManagement.AddSongToDatabase(path);
                if (newSong != null)
                    AllSongs.Add(newSong);
            }
        }

        private void SongListView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (songListView.SelectedItem == null) 
                return;

            var song = (Song)songListView.SelectedItem;

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
    }
}