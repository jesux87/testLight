using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TestNAudio.View.Controls
{
    public class LineControl : Control
    {
        public static readonly DependencyProperty SignalPointsProperty = DependencyProperty.Register("SignalPoints", typeof(IEnumerable<Point>), typeof(LineControl), new PropertyMetadata(default(IEnumerable<Point>)));

        public IEnumerable<Point> SignalPoints
        {
            get
            {
                return (IEnumerable<Point>)GetValue(SignalPointsProperty);
            }
            set
            {
                SetValue(SignalPointsProperty, value);
            }
        }


    }
}
