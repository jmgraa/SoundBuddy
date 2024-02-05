using Microsoft.Win32;

namespace SoundBuddy.ViewModels
{
    internal static class FileManagement
    {
        public static string[]? SelectSongFiles()
        {
            var fileDialog = new OpenFileDialog
            {
                Filter = "MP3 Files | *.mp3",
                Title = "Select the songs you want to add",
                Multiselect = true
            };

            var success = fileDialog.ShowDialog();

            return success == true ? fileDialog.FileNames : null;
        }

        public static string? SelectPlaylistCover()
        {
            var fileDialog = new OpenFileDialog
            {
                Filter = "Image | *.png",
                Title = "Select image for your playlist cover",
                Multiselect = false
            };

            var success = fileDialog.ShowDialog();

            return success == true ? fileDialog.FileName : null;
        }
    }
}
