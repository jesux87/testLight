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
            var _this = (FaderValueControleur)dependencyObject;
            if (_this.ValueChanged != null)
            {
                _this.ValueChanged(_this, new EventArgs());
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
            this.Map = map;
        }

        public FaderValueControleur()
        {
        }

        private void ComputeValue(int valueModifier)
        {
            switch (valueModifier)
            {
                case -2:
                    if (this.map.ControlMode == KeyboardLineControlMode.ThreeThirds) this.Value = this.Minimum;
                    else this.Value = Math.Min(this.Maximum, Math.Max(this.Minimum, this.Value - this.HighValueModifier));
                    break;
                case -1:
                    if (this.map.ControlMode == KeyboardLineControlMode.ThreeThirds) this.Value = this.Minimum + (this.Maximum - this.Minimum) * .3333;
                    else this.Value = Math.Min(this.Maximum, Math.Max(this.Minimum, this.Value - this.LowValueModifier));
                    break;
                case 1:
                    if (this.map.ControlMode == KeyboardLineControlMode.ThreeThirds) this.Value = this.Minimum + (this.Maximum - this.Minimum) * .6667;
                    else this.Value = Math.Min(this.Maximum, Math.Max(this.Minimum, this.Value + this.LowValueModifier));
                    break;
                case 2:
                    if (this.map.ControlMode == KeyboardLineControlMode.ThreeThirds) this.Value = this.Maximum;
                    else this.Value = Math.Min(this.Maximum, Math.Max(this.Minimum, this.Value + this.HighValueModifier));
                    break;
            }
        }

        private void InitCommands()
        {
            this.inputBindings.Clear();
            if (this.map.ModifierKeys == ModifierKeys.None)
            {
                this.inputBindings.Add(new KeyBinding { Command = new RelayCommand(o => this.ComputeValue(-2)), Key = this.map.HighDownKey });
                this.inputBindings.Add(new KeyBinding { Command = new RelayCommand(o => this.ComputeValue(-1)), Key = this.map.LowDownKey });
                this.inputBindings.Add(new KeyBinding { Command = new RelayCommand(o => this.ComputeValue(1)), Key = this.map.LowUpKey });
                this.inputBindings.Add(new KeyBinding { Command = new RelayCommand(o => this.ComputeValue(2)), Key = this.map.HighUpKey });
                return;
            }

            this.inputBindings.Add(new KeyBinding(new RelayCommand(o => this.ComputeValue(-2)), this.map.HighDownKey, this.map.ModifierKeys));
            this.inputBindings.Add(new KeyBinding(new RelayCommand(o => this.ComputeValue(-1)), this.map.LowDownKey, this.map.ModifierKeys));
            this.inputBindings.Add(new KeyBinding(new RelayCommand(o => this.ComputeValue(1)), this.map.LowUpKey, this.map.ModifierKeys));
            this.inputBindings.Add(new KeyBinding(new RelayCommand(o => this.ComputeValue(2)), this.map.HighUpKey, this.map.ModifierKeys));
        }
        
        public ICollection InputBindings
        {
            get
            {
                return this.inputBindings;
            }
        }

        public double LowValueModifier
        {
            get
            {
                return (double)this.GetValue(LowValueModifierProperty);
            }
            set
            {
                this.SetValue(LowValueModifierProperty, value);
            }
        }

        public double HighValueModifier
        {
            get
            {
                return (double)this.GetValue(HighValueModifierProperty);
            }
            set
            {
                this.SetValue(HighValueModifierProperty, value);
            }
        }

        public double Minimum
        {
            get
            {
                return (double)this.GetValue(MinimumProperty);
            }
            set
            {
                this.SetValue(MinimumProperty, value);
            }
        }

        public double Maximum
        {
            get
            {
                return (double)this.GetValue(MaximumProperty);
            }
            set
            {
                this.SetValue(MaximumProperty, value);
            }
        }

        public double Value
        {
            get
            {
                return (double)this.GetValue(ValueProperty);
            }
            set
            {
                this.SetValue(ValueProperty, value);
            }
        }

        public KeyboardLineControlMapping Map
        {
            set
            {
                this.map = value;
                this.InitCommands();
            }
        }
    }
}