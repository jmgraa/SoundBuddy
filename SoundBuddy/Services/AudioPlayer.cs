using NAudio.Wave;
using System.Threading;
using TagLib.Mpeg;

namespace SoundBuddy.Services
{
    public class AudioPlayer : IDisposable
    {
        private IWavePlayer wavePlayer;
        private AudioFileReader audioFile;
        private string filePath;

        private int counter = 0;

        public PlaybackState PlaybackState => wavePlayer?.PlaybackState ?? PlaybackState.Stopped;

        public event EventHandler<PlaybackProgressEventArgs> PlaybackProgressChanged;

        public AudioPlayer()
        {
            wavePlayer = new WaveOutEvent();
            wavePlayer.PlaybackStopped += (sender, args) =>
            {
                if (audioFile != null)
                {
                    audioFile.Position = 0; // Przewiń do początku pliku po zakończeniu odtwarzania
                }
            };
        }

        public void Play(string filePath)
        {
            if (PlaybackState == PlaybackState.Playing)
            {
                Stop();
            }

            this.filePath = filePath;

            audioFile = new AudioFileReader(filePath);
            wavePlayer.Init(audioFile);
            wavePlayer.Play();

            Task.Run(() => MonitorPlaybackProgress());
        }

        public void Pause()
        {
            if (PlaybackState == PlaybackState.Playing)
            {
                wavePlayer.Pause();
            }
        }

        public void Resume()
        {
            if (PlaybackState == PlaybackState.Paused)
            {
                wavePlayer.Play();
            }
        }

        public void Stop()
        {
            wavePlayer?.Stop();
        }

        private async Task MonitorPlaybackProgress()
        {
            while (PlaybackState == PlaybackState.Playing)
            {
                OnPlaybackProgressChanged(new PlaybackProgressEventArgs(audioFile.Position, audioFile.Length, audioFile.WaveFormat.SampleRate));
                await Task.Delay(100);
            }
        }

        protected virtual void OnPlaybackProgressChanged(PlaybackProgressEventArgs e)
        {
            PlaybackProgressChanged?.Invoke(this, e);
        }

        public void Dispose()
        {
            Stop();
            wavePlayer?.Dispose();
            audioFile?.Dispose();
        }
    }

    public class PlaybackProgressEventArgs : EventArgs
    {
        public long CurrentPosition { get; }
        public long TotalLength { get; }
        public int SampleRate { get; }

        public PlaybackProgressEventArgs(long currentPosition, long totalLength, int sampleRate)
        {
            CurrentPosition = currentPosition;
            TotalLength = totalLength;
            SampleRate = sampleRate;
        }

        public double GetElapsedTimeInSeconds()
        {
            if (SampleRate == 0)
            {
                return 0;
            }

            double elapsedTime = (double)CurrentPosition / SampleRate;
            return elapsedTime;
        }

        public double GetTotalTimeInSeconds()
        {
            return (double)TotalLength / SampleRate;
        }
    }

}
