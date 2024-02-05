using SoundBuddy.ViewModels;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace SoundBuddy.Views.PopUps
{
	public partial class CreatePlaylistPopUp
    {
        private readonly MainWindow _window;

        public CreatePlaylistPopUp(MainWindow window)
        {
            InitializeComponent();

            _window = window;

            ImgCover.Source = Tools.InsertImage;
        }

        private void BtnCreate_OnClick(object sender, RoutedEventArgs e)
        {
            var name = TxtBoxName.Text.Trim();
            var description = TxtBxDescription.Text.Trim();
            var cover = ImgCover.Source != Tools.InsertImage ? ImgCover.Source as BitmapImage : null;

            if (!ValidateData(name, description)) return;

            SongManagement.AddPlaylistToDatabase(name, description, cover);
            _window.SoundyFacade.RefreshPlaylistPage();
            Close();
        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private static bool ValidateData(string name, string description)
        {
            if (string.IsNullOrEmpty(name))
                return false;

            return name.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)) 
                   && description.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)) 
                   && name.Length is >= 3 and <= 30;
        }

        private void ImgCover_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var path = FileManagement.SelectPlaylistCover();

            if (path != null)
                ImgCover.Source = Tools.LoadImageFromAbsolutePath(path);
        }
    }
}
