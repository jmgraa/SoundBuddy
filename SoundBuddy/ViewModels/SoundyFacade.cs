﻿using NAudio.Wave;
using SoundBuddy.Models;
using SoundBuddy.Services;
using SoundBuddy.Views.UserControls;
using System.Collections.ObjectModel;

namespace SoundBuddy.ViewModels
{
    public class SoundyFacade(MainWindow window)
    {
        private readonly DisplayController _displayController = new(window);
        private readonly PageController _pageController = new(window);
        private readonly AudioPlayer _audioPlayer = new(window);
        private readonly QueueController _queueController = new(window);

        // PAGE CONTROLLER
        public void SwitchToLocalFilesPage()
        {
            _pageController.SwitchToLocalFilesPage();
        }

        public void SwitchToPlaylistListPage()
        {
            _pageController.SwitchToPlaylistListPage();
        }

        public void SwitchToSettingsPage()
        {
            _pageController.SwitchToSettingsPage();
        }

        public void AddSongsToList(string[] paths)
        {
            _pageController.AddSongsToList(paths);
        }

        public void LoadSelectedPlaylistPage(Playlist playlist)
        {
            _pageController.LoadSelectedPlaylistPage(playlist);
        }

        public ObservableCollection<PlaylistUserControl> GetUserControls()
        {
            return _pageController.getUserControls();
        }
        // END OF PAGE CONTROLLER

        // AUDIO PLAYER
        public void PlaySong(Song song)
        {
            _audioPlayer.Load(song.Path);
            _audioPlayer.Play();

            _displayController.ShowCurrentSongData(song);
        }

        public void Play()
        {
            _audioPlayer.Play();
        }

        public void Pause()
        {
            _audioPlayer.Pause();
        }

        public void Stop()
        {
            _audioPlayer.Stop();
        }
        // END OF AUDIO PLAYER

        // DISPLAY CONTROLLER
        public void MinimizeWindow()
        {
            _displayController.MinimizeWindow();
        }

        public void MaximizeWindow()
        {
            _displayController.MaximizeWindow();
        }

        public void CloseWindow()
        {
            _displayController.CloseWindow();
        }

        public void ResetTimeLabels()
        {
            _displayController.ResetTimeLabels();
        }

        public void UpdateTimeLabel(AudioFileReader audioFile)
        {
            _displayController.UpdateTimeLabel(audioFile);
        }

        public void UpdateProgressBar(AudioFileReader audioFile)
        {
            _displayController.UpdateProgressBar(audioFile);
        }
        // END OF DISPLAY CONTROLLER

        // FILE CONTROLLER
        public string[]? SelectSongFiles()
        {
            return FileController.SelectSongFiles();
        }
        // END OF FILE CONTROLLER

        // QUEUE CONTROLLER
        public Song? GetNextSongToPlay()
        {
            return _queueController.GetNextSongToPlay();
        }

        public void ChangeCurrentQueue(ObservableCollection<Song> newQueue)
        {
            _queueController.ChangeCurrentQueue(newQueue);
        }
        // END OF QUEUE CONTROLLER
    }
}
