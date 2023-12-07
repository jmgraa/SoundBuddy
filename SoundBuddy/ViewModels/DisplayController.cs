using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using SoundBuddy.Models;

namespace SoundBuddy.ViewModels
{
    internal class DisplayController
    {
        private readonly MainWindow _window;
        private readonly BitmapImage _soundBuddyLogo;

        public DisplayController(MainWindow window)
        {
            _window = window;

            _soundBuddyLogo = new BitmapImage();
            _soundBuddyLogo.BeginInit();
            // TODO fix path
            _soundBuddyLogo.UriSource = new Uri($"{AppDomain.CurrentDomain.BaseDirectory}SoundBuddy.png", UriKind.RelativeOrAbsolute);
            _soundBuddyLogo.EndInit();
        }

        public void MinimizeWindow()
        {
            _window.WindowState = WindowState.Minimized;
        }

        public void MaximizeWindow()
        {
            _window.WindowState = _window.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }

        public void CloseWindow()
        {
            _window.Close();
        }

        public void ShowCurrentSongData(Song song)
        {
            _window.CurrentTitle.Content = song.Title != null ? song.Title : "Unknown title";
            _window.CurrentArtist.Content = song.Artists != null ? song.Artists : "Unknown artist";
            _window.CurrentAlbum.Content = song.Album != null ? song.Album : "Unknown album";

            if (song.Cover != null)
            {
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = new MemoryStream(song.Cover.Data.Data);
                bitmapImage.EndInit();

                _window.CurrentCover.Source = bitmapImage;
            }
            else
            {
                _window.CurrentCover.Source = _soundBuddyLogo;
            }
        }
    }
}
