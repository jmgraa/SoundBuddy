using Microsoft.Win32;

namespace SoundBuddy.ViewModels
{
    internal class FileController()
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
    }
}
