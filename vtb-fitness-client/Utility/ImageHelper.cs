using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;

namespace vtb_fitness_client.Utility
{
    public static class ImageHelper
    {
        public static BitmapImage GetImage(byte[]? image) =>
            image == null ? GetDefaultImage() : LoadImageFromBytes(image);

        private static BitmapImage LoadImageFromBytes(byte[] image)
        {
            using var stream = new MemoryStream(image);
            var outputImage = new BitmapImage();
            outputImage.BeginInit();
            outputImage.StreamSource = stream;
            outputImage.CacheOption = BitmapCacheOption.OnLoad;
            outputImage.EndInit();
            return outputImage;
        }

        public static BitmapImage GetImageFromPath(string path) =>
            File.Exists(path) ? new BitmapImage(new Uri($"pack://application:,,,{path}")) : GetDefaultImage();

        public static byte[]? CreateImage(string path)
        {
            if (!File.Exists(path)) return null;
            return File.ReadAllBytes(path);
        }

        public static bool IsWidescreenAspectRatio(BitmapImage bitmapImage) =>
            Math.Abs(GetAspectRatio(bitmapImage) - 1.7778) <= 0.05;

        public static bool IsEqualAspectRatio(BitmapImage bitmapImage) =>
            Math.Abs(GetAspectRatio(bitmapImage) - 1.0) <= 0.01;

        private static double GetAspectRatio(BitmapImage bitmapImage) =>
            (double)bitmapImage.PixelWidth / bitmapImage.PixelHeight;

        private static BitmapImage GetDefaultImage() =>
            new BitmapImage(new Uri("pack://application:,,,/Assets/Images/default-pfp.png"));
    }
}
