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

namespace SoundBuddy
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Song> AllSongs = SongManagement.GetAllSongs();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            songListView.ItemsSource = AllSongs;
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

                CurrentTitle.Content = song.Title;
                CurrentArtist.Content = song.Artist;
                CurrentAlbum.Content = song.Album;
            }
        }
    }
}