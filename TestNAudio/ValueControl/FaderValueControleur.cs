using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

using WpfTools;

namespace TestNAudio.ValueControl
{
    public class FaderValueControleur : DependencyObject, IValueControleur
    {
        public event EventHandler ValueChanged;

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double), typeof(FaderValueControleur), new PropertyMetadata(default(double), ValueChangedCallback));

        private static void ValueChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var @this = (FaderValueControleur)dependencyObject;
            if (@this.ValueChanged != null)
            {
                @this.ValueChanged(@this, new EventArgs());
            }
        }

        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register("Maximum", typeof(double), typeof(FaderValueControleur), new PropertyMetadata(1.0));

        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register("Minimum", typeof(double), typeof(FaderValueControleur), new PropertyMetadata(0.0));

        public static readonly DependencyProperty HighValueModifierProperty = DependencyProperty.Register("HighValueModifier", typeof(double), typeof(FaderValueControleur), new PropertyMetadata(.1));

        public static readonly DependencyProperty LowValueModifierProperty = DependencyProperty.Register("LowValueModifier", typeof(double), typeof(FaderValueControleur), new PropertyMetadata(.02));

        private KeyboardLineControlMapping map;

        private readonly Collection<InputBinding> inputBindings = new Collection<InputBinding>();

        public FaderValueControleur(KeyboardLineControlMapping map):this()
        {
            Map = map;
        }

        public FaderValueControleur()
        {
        }

        private void ComputeValue(int valueModifier)
        {
            switch (valueModifier)
            {
                case -2:
                    if (map.ControlMode == KeyboardLineControlMode.ThreeThirds) Value = Minimum;
                    else Value = Math.Min(Maximum, Math.Max(Minimum, Value - HighValueModifier));
                    break;
                case -1:
                    if (map.ControlMode == KeyboardLineControlMode.ThreeThirds) Value = Minimum + (Maximum - Minimum) * .3333;
                    else Value = Math.Min(Maximum, Math.Max(Minimum, Value - LowValueModifier));
                    break;
                case 1:
                    if (map.ControlMode == KeyboardLineControlMode.ThreeThirds) Value = Minimum + (Maximum - Minimum) * .6667;
                    else Value = Math.Min(Maximum, Math.Max(Minimum, Value + LowValueModifier));
                    break;
                case 2:
                    if (map.ControlMode == KeyboardLineControlMode.ThreeThirds) Value = Maximum;
                    else Value = Math.Min(Maximum, Math.Max(Minimum, Value + HighValueModifier));
                    break;
            }
        }

        private void InitCommands()
        {
            inputBindings.Clear();
            if (map.ModifierKeys == ModifierKeys.None)
            {
                inputBindings.Add(new KeyBinding { Command = new RelayCommand(o => ComputeValue(-2)), Key = map.HighDownKey });
                inputBindings.Add(new KeyBinding { Command = new RelayCommand(o => ComputeValue(-1)), Key = map.LowDownKey });
                inputBindings.Add(new KeyBinding { Command = new RelayCommand(o => ComputeValue(1)), Key = map.LowUpKey });
                inputBindings.Add(new KeyBinding { Command = new RelayCommand(o => ComputeValue(2)), Key = map.HighUpKey });
                return;
            }

            inputBindings.Add(new KeyBinding(new RelayCommand(o => ComputeValue(-2)), map.HighDownKey, map.ModifierKeys));
            inputBindings.Add(new KeyBinding(new RelayCommand(o => ComputeValue(-1)), map.LowDownKey, map.ModifierKeys));
            inputBindings.Add(new KeyBinding(new RelayCommand(o => ComputeValue(1)), map.LowUpKey, map.ModifierKeys));
            inputBindings.Add(new KeyBinding(new RelayCommand(o => ComputeValue(2)), map.HighUpKey, map.ModifierKeys));
        }
        
        public ICollection InputBindings
        {
            get
            {
                return inputBindings;
            }
        }

        public double LowValueModifier
        {
            get
            {
                return (double)GetValue(LowValueModifierProperty);
            }
            set
            {
                SetValue(LowValueModifierProperty, value);
            }
        }

        public double HighValueModifier
        {
            get
            {
                return (double)GetValue(HighValueModifierProperty);
            }
            set
            {
                SetValue(HighValueModifierProperty, value);
            }
        }

        public double Minimum
        {
            get
            {
                return (double)GetValue(MinimumProperty);
            }
            set
            {
                SetValue(MinimumProperty, value);
            }
        }

        public double Maximum
        {
            get
            {
                return (double)GetValue(MaximumProperty);
            }
            set
            {
                SetValue(MaximumProperty, value);
            }
        }

        public double Value
        {
            get
            {
                return (double)GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, value);
            }
        }

        public KeyboardLineControlMapping Map
        {
            set
            {
                map = value;
                InitCommands();
            }
        }
    }
}