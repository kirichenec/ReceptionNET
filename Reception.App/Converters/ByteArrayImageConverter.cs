using Avalonia.Data.Converters;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;

namespace Reception.App.Converters
{
    public class ByteArrayImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte[] data)
            {
                using var ms = new MemoryStream(data);
                return new Bitmap(ms);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            byte[] data = Array.Empty<byte>();

            if (value is Bitmap image)
            {
                using var stream = new MemoryStream();
                image.Save(stream, ImageFormat.Jpeg);
                data = stream.GetBuffer();
            }

            return data;
        }
    }
}