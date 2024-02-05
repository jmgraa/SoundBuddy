using NAudio.Wave;
using SoundBuddy.Models;
using SoundBuddy.Views.PopUps;
using System.Windows;
using System.Windows.Media;

namespace SoundBuddy.ViewModels
{
    internal class DisplayController
    {
        private readonly MainWindow _window;

        public DisplayController(MainWindow window)
        {
            _window = window;

            _window.CurrentCover.Source = Tools.SoundBuddyLogo;
            _window.CurrentTitle.Content = "Welcome to SoundBuddy";
            _window.CurrentArtist.Content = "Enjoy listening :)";
            _window.CurrentAlbum.Content = "";
            _window.CurrentOtherData.Content = "";
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
            _window.CurrentTitle.Content = song.Title;
            _window.CurrentArtist.Content = song.Artists;
            _window.CurrentAlbum.Content = song.Album;
            _window.CurrentCover.Source = song.Cover ?? Tools.SoundBuddyLogo;
            _window.CurrentOtherData.Content = $"{song.Genre}, {song.Year}";
        }

        public void UpdateTimeLabel(AudioFileReader audioFile)
        {
            _window.LbCurrentTime.Content = Tools.FormatTime(audioFile.CurrentTime);
            _window.LbLeftTime.Content = $"-{Tools.FormatTime(audioFile.TotalTime - audioFile.CurrentTime)}";
        }

        public void UpdateProgressBar(AudioFileReader audioFile)
        {
            _window.ProgressBar.Value = audioFile.Position * 100 / audioFile.Length;
        }

        public void ResetTimeLabels()
        {
            _window.LbCurrentTime.Content = "0:00";
            _window.LbLeftTime.Content = "0:00";
        }

        // TODO
        public void Mute()
        {
            _window.BtnMute.Foreground = _window.BtnMute.Foreground == Brushes.White ? Brushes.Red : Brushes.White;
        }

        public void ChangeRandomModeButton(bool randomMode)
        {
            _window.BtnRandom.Opacity = randomMode ? 1 : 0.4;
        }

        public void CreatePlaylist()
        {
            var createPlaylistWindow = new CreatePlaylistPopUp(_window);
            createPlaylistWindow.Show();
        }
    }
}
