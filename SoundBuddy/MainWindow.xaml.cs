using SoundBuddy.Models;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using SoundBuddy.ViewModels;
using System.IO;
using SoundBuddy.Services;

namespace SoundBuddy
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Song> AllSongs = SongManagement.GetAllSongs();

        AudioPlayer _audioPlayer = new AudioPlayer();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            songListView.ItemsSource = AllSongs;

            _audioPlayer.PlaybackProgressChanged += (sender, args) =>
            {
                double elapsedTime = args.GetElapsedTimeInSeconds();
                double totalTime = args.GetTotalTimeInSeconds();

                // Użycie Dispatcher.Invoke do przejścia do wątku głównego
                Dispatcher.Invoke(() =>
                {
                    LbCurrentTime.Content = $"{elapsedTime:F0} s";
                    LbLeftTime.Content = $"{totalTime:F0} s";
                });
            };

        }

        private void BtnAddSongs_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Filter = "MP3 Files | *.mp3",
                Title = "Select the songs you want to add",
                Multiselect = true
            };

            var success = fileDialog.ShowDialog();
            if (success == true)
            {
                string[] paths = fileDialog.FileNames;

                foreach (var path in paths)
                {
                    var newSong = SongManagement.AddSongToDatabase(path);
                    if (newSong != null)
                        AllSongs.Add(newSong);
                }
            }
        }

        private void SongListView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (songListView.SelectedItem != null)
            {
                Song song = (Song)songListView.SelectedItem;

                _audioPlayer.Play(song.Path);

                CurrentTitle.Content = song.Title;
                CurrentArtist.Content = song.Artist;
                CurrentAlbum.Content = song.Album;
            }
        }

        private void BtnStop_OnClick(object sender, RoutedEventArgs e)
        {
            _audioPlayer.Stop();
        }

        private void BtnPlay_OnClick(object sender, RoutedEventArgs e)
        {
            _audioPlayer.Resume();
        }

        private void BtnPause_OnClick(object sender, RoutedEventArgs e)
        {
            _audioPlayer.Pause();
        }
    }
}