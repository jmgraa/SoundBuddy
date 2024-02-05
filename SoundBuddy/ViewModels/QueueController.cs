using SoundBuddy.Models;
using System.Collections.ObjectModel;

namespace SoundBuddy.ViewModels
{
    public class QueueController(MainWindow window)
    {
        private readonly ObservableCollection<Song> _playedSongs = [];
        private ObservableCollection<Song> _currentQueue = SongManagement.GetAllSongs();

        private Song? _currentSong;

        private int _songIndex;
        private bool _randomSong;

        public Song? GetNextSongToPlay()
        {
            if (_currentQueue.Count > 0)
            {
                if (_currentSong != null && (_playedSongs.Count == 0 || _playedSongs.Last() != _currentSong))
                    _playedSongs.Add(_currentSong);

                _currentSong = _randomSong ? GetRandomSong() : GetNextSong();
                return _currentSong;
            }

            window.SoundyFacade.Stop();
            _currentSong = null;
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
            var index = newQueue.IndexOf(_currentSong!);

            var collection = new ObservableCollection<Song>();

            for (var i = index + 1; i < newQueue.Count; i++)
            {
                collection.Add(newQueue[i]);
            }

            for (var i = 0; i < index; i++)
            {
                collection.Add(newQueue[i]);
            }
            
            _currentQueue = collection;
            _playedSongs.Clear();
        }

        public bool ChangeRandomMode()
        {
            _randomSong = !_randomSong;
            return _randomSong;
        }

        public void SetCurrentSongInQueue(Song song)
        { 
            _currentSong = song;
        }
    }
}
