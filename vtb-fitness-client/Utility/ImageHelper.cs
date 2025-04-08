using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;
using Microsoft.Win32;
using vtb_fitness_client.Windows;

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

        //public static BitmapImage GetImageFromPath(string path) =>
        //    File.Exists(path) ? new BitmapImage(new Uri($"pack://application:,,,{path}")) : GetDefaultImage();

        public static BitmapImage GetImageFromPath(string path) =>
            File.Exists(path) ? new BitmapImage(new Uri(path)) : GetDefaultImage();

        public static byte[]? CreateImage(string path)
        {
            if (!File.Exists(path)) return null;
            return File.ReadAllBytes(path);
        }

        public static (string? FileName, byte[]? RawImage) GetImageFromFileDialog(Window? senderWindow = null)
        {
            var ofd = new OpenFileDialog
            {
                Title = "Выберите изображение",
                Filter = "Images (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png",
                Multiselect = false
            };
            if (ofd.ShowDialog() == true)
            {
                var path = ofd.FileName;
                var bmp = GetImageFromPath(path);
                if (!IsEqualAspectRatio(bmp))
                {
                    new DialogWindow(senderWindow,
                                     "Ошибка",
                                     "Изображение должно иметь соотношение сторон 1:1",
                                     DialogWindowButtons.Ok,
                                     DialogWindowType.Error) { Owner = senderWindow }.ShowDialog();
                    return (null, null);
                }
                var temp = path.Split('\\');

                var fileName = temp.Last();
                var rawImage = CreateImage(path);
                return (fileName, rawImage);
            }
            return (null, null);
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
