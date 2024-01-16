using System.IO;
using System.Security.Policy;
using System.Windows.Media.Imaging;
using SoundBuddy.Services;

namespace SoundBuddy
{
    internal static class Tools
    {
        public static readonly BitmapImage SoundBuddyLogo = LoadImageFromPath("SoundBuddy");
        public static readonly BitmapImage InsertImage = LoadImageFromPath("InsertImage");
        public static readonly BitmapImage NoImage = LoadImageFromPath("NoImage");

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

        public static BitmapImage LoadImageFromAbsolutePath(string path)
        {
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
            bitmapImage.EndInit();

            return bitmapImage;
        }

        public static byte[] ConvertBitmapImageToByteArray(BitmapImage bitmapImage)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                encoder.Save(stream);

                return stream.ToArray();
            }
        }


        public static string FormatTime(TimeSpan time)
        {
            return $"{(int)time.TotalMinutes:D2}:{time.Seconds:D2}";
        }
    }
}
