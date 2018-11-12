using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace TestNAudio
{
    [ValueConversion(typeof(byte), typeof(double))]
    public class FaderValueToOpacityConverter : IValueConverter
    {

        private double maxFactor = .8;

        public double MaxFactor
        {
            set
            {
                maxFactor = value;
            }
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var val = (byte)value;
            return val * maxFactor / 0xff;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var val = (double)value;
            return (byte)(val * 0xff / maxFactor);
        }
    }
    
    [ValueConversion(typeof(byte),typeof(Color))]
    public class FaderValueToColorConverter : IValueConverter
    {
        private bool redDepends = true;
        private bool greenDepends = true;
        private bool blueDepends = true;

        private byte redBaseValue = 0xff;
        private byte greenBaseValue = 0xff;
        private byte blueBaseValue = 0xff;

        public bool RedDepends
        {
            set
            {
                redDepends = value;
            }
        }

        public bool GreenDepends
        {
            set
            {
                greenDepends = value;
            }
        }

        public bool BlueDepends
        {
            set
            {
                blueDepends = value;
            }
        }

        public byte RedBaseValue
        {
            set
            {
                redBaseValue = value;
            }
        }


        public byte GreenBaseValue
        {
            set
            {
                greenBaseValue = value;
            }
        }

        public byte BlueBaseValue
        {
            set
            {
                blueBaseValue = value;
            }
        }


        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var val = (byte)value;
            var r = redDepends ? (byte)(redBaseValue * val / 0xff) : redBaseValue;
            var g = greenDepends ? (byte)(greenBaseValue * val / 0xff) : greenBaseValue;
            var b = blueDepends ? (byte)(blueBaseValue * val / 0xff) : blueBaseValue;
            return Color.FromRgb(r, g, b);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
