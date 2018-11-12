using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace TestNAudio
{
    [ValueConversion(typeof(byte[]), typeof(Color))]
    public class ThreeByteToColorConverter: IMultiValueConverter
    {
        public byte Alpha { private get; set; }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var red = (byte)values[0];
            var green = (byte)values[1];
            var blue = (byte)values[2];
            return new Color
                       {
                           A = Alpha,
                           R = red,
                           G = green,
                           B = blue
                       };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            var color = (Color)value;
            return new[] { color.R, color.G, color.B }.Cast<object>().ToArray();
        }
    }
}
