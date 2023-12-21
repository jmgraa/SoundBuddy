using System.Drawing;
using System.Printing.IndexedProperties;
using System.Windows;
using System.Windows.Media.Imaging;
using NAudio.CoreAudioApi.Interfaces;
using SoundBuddy.Models;

namespace SoundBuddy.Views.PopUps
{
    public partial class CreatePlaylistPopUp : Window
    {
        public delegate void ReturnValueHandler((string, string, BitmapImage)? data);
        public event ReturnValueHandler ReturnValueEvent;

        public CreatePlaylistPopUp()
        {
            InitializeComponent();
        }

        private void BtnCreate_OnClick(object sender, RoutedEventArgs e)
        {

            var data = new ValueTuple<string, string, BitmapImage>(TxtBoxName.Text, TxtBxDescription.Text, null);
            ReturnValueEvent?.Invoke(data);

            Close();
        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            ReturnValueEvent?.Invoke(null);
            Close();
        }

        private bool CheckDataCorectness((string, string, string) data)
        {

        }
    }
}
