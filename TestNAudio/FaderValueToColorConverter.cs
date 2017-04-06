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
                this.maxFactor = value;
            }
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var val = (byte)value;
            return val * this.maxFactor / 0xff;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var val = (double)value;
            return (byte)(val * 0xff / this.maxFactor);
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
                this.redDepends = value;
            }
        }

        public bool GreenDepends
        {
            set
            {
                this.greenDepends = value;
            }
        }

        public bool BlueDepends
        {
            set
            {
                this.blueDepends = value;
            }
        }

        public byte RedBaseValue
        {
            set
            {
                this.redBaseValue = value;
            }
        }


        public byte GreenBaseValue
        {
            set
            {
                this.greenBaseValue = value;
            }
        }

        public byte BlueBaseValue
        {
            set
            {
                this.blueBaseValue = value;
            }
        }


        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var val = (byte)value;
            var r = this.redDepends ? (byte)(this.redBaseValue * val / 0xff) : this.redBaseValue;
            var g = this.greenDepends ? (byte)(this.greenBaseValue * val / 0xff) : this.greenBaseValue;
            var b = this.blueDepends ? (byte)(this.blueBaseValue * val / 0xff) : this.blueBaseValue;
            return Color.FromRgb(r, g, b);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
