using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using System.Globalization;

namespace Reception.App.Converters
{
    public class ByteArrayImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte[] data && data.Length > 0)
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
                image.Save(stream);
                data = stream.GetBuffer();
            }

            return data;
        }
    }
}