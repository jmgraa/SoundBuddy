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
                Console.WriteLine($"Błąd ładowania obrazu: {ex.Message}");
            }

            return bitmapImage;
        }

        // TODO fix path
        public static BitmapImage LoadImageFromPath(string path)
        {
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri($"{AppDomain.CurrentDomain.BaseDirectory}{path}.png", UriKind.RelativeOrAbsolute);
            bitmapImage.EndInit();

            return bitmapImage;
        }

        public static string FormatTime(TimeSpan time)
        {
            return $"{(int)time.TotalMinutes:D2}:{time.Seconds:D2}";
        }
    }
}
