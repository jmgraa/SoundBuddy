using SoundBuddy.Models;
using System.Collections.ObjectModel;

namespace SoundBuddy.ViewModels
{
    public class QueueController(MainWindow window)
    {
        private ObservableCollection<Song> _wholeQueue = SongManagement.GetAllSongs();
        private ObservableCollection<Song> _currentQueue = SongManagement.GetAllSongs();
        private int _songIndex;
        private bool _randomSong;

        public Song? GetNextSongToPlay()
        {
            if (_currentQueue.Count != 0)
                return _randomSong ? GetRandomSong() : GetNextSong();

            window.SoundyFacade.Stop();
            return null;
        }

        private Song GetRandomSong()
        {
            var random = new Random();

            var randomIndex = random.Next(0, _currentQueue.Count);
            var song = _currentQueue[randomIndex];

            _currentQueue.RemoveAt(randomIndex);
            _songIndex = randomIndex;

            return song;
        }

        private Song GetNextSong()
        {
            if (_songIndex >= _currentQueue.Count)
                _songIndex = 0;

            var song = _currentQueue[_songIndex];
            _currentQueue.RemoveAt(_songIndex);

            return song;
        }

        public void ChangeCurrentQueue(ObservableCollection<Song> newQueue)
        {
            _wholeQueue = newQueue;
            _currentQueue = new ObservableCollection<Song>(_wholeQueue);
        }

        public void ChangeRandomMode(bool randomMode)
        {
            _randomSong = randomMode;
        }
    }
}
