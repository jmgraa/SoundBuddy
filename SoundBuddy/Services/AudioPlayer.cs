using NAudio.Wave;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SoundBuddy.Services
{
    public class AudioPlayer
    {
        private WaveOutEvent? _wavePlayer;
        private AudioFileReader? _audioFile;
        private readonly DispatcherTimer _timer = new();
        private readonly Label _currentTimeLabel;
        private readonly Label _leftTimeLabel;

        public AudioPlayer(Label currentTimeLabel, Label leftTimeLabel)
        {
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;

            _currentTimeLabel = currentTimeLabel;
            _leftTimeLabel = leftTimeLabel;
            UpdateTimeLabel();
            
        }

        public void Load(string filePath)
        {
            if (File.Exists(filePath) && Path.GetExtension(filePath).ToLower() == ".mp3")
            {
                Dispose();

                _audioFile = new AudioFileReader(filePath);
                _wavePlayer = new WaveOutEvent();
                _wavePlayer.Init(_audioFile);
                UpdateTimeLabel();
            }
            else
                MessageBox.Show("Error! Cannot load");
        }

        public void Play()
        {
            if (_wavePlayer == null || _audioFile == null)
                return;

            _wavePlayer.Play();
            _timer.Start();
        }

        public void Pause()
        {
            if (_wavePlayer == null)
                return;

            _wavePlayer.Pause();
            _timer.Stop();
        }

        public void Stop()
        {
            if (_wavePlayer == null || _audioFile == null) 
                return;

            _wavePlayer.Stop();
            _timer.Stop();
            _audioFile.Position = 0;
            UpdateTimeLabel();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_wavePlayer != null && _audioFile != null)
                UpdateTimeLabel();
        }

        // TODO implement leftTimeLabel 
        private void UpdateTimeLabel()
        {
            if (_audioFile != null)
                _currentTimeLabel.Content = FormatTime(_audioFile.CurrentTime);
        }

        private static string FormatTime(TimeSpan time)
        {
            return $"{(int)time.TotalMinutes:D2}:{time.Seconds:D2}";
        }

        public void Dispose()
        {
            Stop();

            if (_audioFile != null)
            {
                _audioFile.Dispose();
                _audioFile = null;
            }

            if (_wavePlayer != null)
            {
                _wavePlayer.Dispose();
                _wavePlayer = null;
            }

            _currentTimeLabel.Content = "00:00";
        }
    }

}
