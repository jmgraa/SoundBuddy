using NAudio.Wave;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SoundBuddy.Services
{
    public class AudioPlayer
    {
        private readonly MainWindow _window;

        private WaveOutEvent? _wavePlayer;
        private AudioFileReader? _audioFile;
        private readonly DispatcherTimer _timer = new();
        private readonly Slider _volumeSlider;

        public AudioPlayer(MainWindow window)
        {
            _window = window;
            _volumeSlider = _window.VolumeSlider;

            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;

            _volumeSlider.ValueChanged += VolumeSlider_ValueChanged;
        }

        public void Load(string filePath)
        {
            if (File.Exists(filePath) && Path.GetExtension(filePath).ToLower() == ".mp3")
            {
                Dispose();

                _audioFile = new AudioFileReader(filePath);
                _wavePlayer = new WaveOutEvent();
                _wavePlayer.Init(_audioFile);
                _window.SoundyFacade.UpdateTimeLabel(_audioFile);
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

            _window.SoundyFacade.UpdateTimeLabel(_audioFile);
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

            _window.SoundyFacade.ResetTimeLabels();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_wavePlayer == null || _audioFile == null)
                return;

            _window.SoundyFacade.UpdateTimeLabel(_audioFile);
            _window.SoundyFacade.UpdateProgressBar(_audioFile);
        }

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_wavePlayer != null)
                _wavePlayer.Volume = (float)_volumeSlider.Value;
        }
    }
}
