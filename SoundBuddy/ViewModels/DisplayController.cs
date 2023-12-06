using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using SoundBuddy.Models;

namespace SoundBuddy.ViewModels
{
    internal class DisplayController(MainWindow window)
    {
        public void MinimizeWindow()
        {
            window.WindowState = WindowState.Minimized;
        }

        public void MaximizeWindow()
        {
            window.WindowState = window.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }

        public void CloseWindow()
        {
            window.Close();
        }

        public void ShowCurrentSongData(Song song)
        {
            window.CurrentTitle.Content = song.Title != null ? song.Title : "Unknown title";
            window.CurrentArtist.Content = song.Artists != null ? song.Artists : "Unknown artist";
            window.CurrentAlbum.Content = song.Album != null ? song.Album : "Unknown album";

            if (song.Cover != null)
            {
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = new MemoryStream(song.Cover.Data.Data);
                bitmapImage.EndInit();

                window.CurrentCover.Source = bitmapImage;
            }
            else
            {

            }
            


        }
    }
}
