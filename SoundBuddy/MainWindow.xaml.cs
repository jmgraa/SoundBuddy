using SoundBuddy.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SoundBuddy
{
	public partial class MainWindow
    {
        internal readonly SoundyFacade SoundyFacade;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
            SoundyFacade = new SoundyFacade(this);
        }

        private void ButtonClicked(object sender, RoutedEventArgs e)
        {
	        if (sender is not Button button) return;

	        var buttonName = button.Name;

	        switch (buttonName)
	        {
                case "BtnAddSongs":
	                SoundyFacade.SwitchToLocalFilesPage();
	                var paths = SoundyFacade.SelectSongFiles();
	                if (paths == null) return;
	                SoundyFacade.AddSongsToList(paths);
	                break;
                case "BtnStop":
	                SoundyFacade.Stop();
	                break;
                case "BtnPlay":
	                SoundyFacade.Play();
	                break;
                case "BtnPause":
	                SoundyFacade.Pause();
	                break;
                case "BtnMinimize":
	                SoundyFacade.MinimizeWindow();
	                break;
                case "BtnMaximize":
	                SoundyFacade.MaximizeWindow();
	                break;
                case "BtnClose":
	                SoundyFacade.CloseWindow();
	                break;
                case "BtnPlaylists":
	                SoundyFacade.SwitchToPlaylistListPage();
	                break;
                case "BtnLocalFiles":
	                SoundyFacade.SwitchToLocalFilesPage();
	                break;
                case "BtnCreatePlaylist":
	                SoundyFacade.SwitchToPlaylistListPage();
	                SoundyFacade.CreatePlaylist();
	                break;
                case "BtnMute":
	                SoundyFacade.Mute();
	                break;
                case "BtnPrevious":
	                SoundyFacade.Restart();
	                break;
                case "BtnNext":
	                SoundyFacade.PlaySong(SoundyFacade.GetNextSongToPlay());
	                break;
                case "BtnRandom":
	                SoundyFacade.ChangeRandomMode();
	                break;
	        }
        }

        private void MainWindow_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}