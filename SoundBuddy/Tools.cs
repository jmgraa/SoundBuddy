using System.IO;
using System.Windows.Media.Imaging;

namespace SoundBuddy
{
    internal static class Tools
    {
        public static BitmapImage LoadImageFromBlob(byte[] imageBlob)
        {
            var bitmapImage = new BitmapImage();

            try
            {
                using (MemoryStream memoryStream = new MemoryStream(imageBlob))
                {
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = memoryStream;
                    bitmapImage.EndInit();
                    bitmapImage.Freeze();
                }
            }
            catch (Exception ex)
            {
                // Obsłuż błędy ładowania obrazu
                Console.WriteLine($"Błąd ładowania obrazu: {ex.Message}");
            }

            return bitmapImage;
        }

        public static string FormatTime(TimeSpan time)
        {
            return $"{(int)time.TotalMinutes:D2}:{time.Seconds:D2}";
        }
    }
}
