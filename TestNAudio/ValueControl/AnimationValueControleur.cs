using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

using TestNAudio.Model;

using WpfTools;

namespace TestNAudio
{
    public class AnimationsControleur
    {

        private readonly Collection<InputBinding> inputBindings = new Collection<InputBinding>();
        
        private InputBinding inputBinding;

        private ICollection<AnimationValueControleur> controlleurs;

        public ICollection InputBindings
        {
            get
            {
                return this.inputBindings;
            }
        }

        public ICollection<AnimationValueControleur> Controlleurs
        {
            get
            {
                return this.controlleurs;
            }
        }

        public KeyGesture Gesture
        {
            set
            {
                if (this.inputBinding != null)
                    this.inputBindings.Clear();
                this.inputBinding = new InputBinding(new RelayCommand(p => this.LaunchSequences()), value);
                this.inputBindings.Add(this.inputBinding);
            }
        }

        public AnimationsControleur(KeyGesture gesture) : this()
        {
            this.Gesture = gesture;
        }

        public AnimationsControleur()
        {
        }

        private void LaunchSequences()
        {
            foreach (var controleur in Controlleurs)
            {
                controleur.LaunchSequence();
            }
        }
    }

    public class AnimationValueControleur : Animatable//, IValueControleur
    {
        public static readonly DependencyProperty DurationProperty = DependencyProperty.Register("Duration", typeof(Duration), typeof(AnimationValueControleur), new PropertyMetadata(default(Duration)));

        public Duration Duration
        {
            get
            {
                return (Duration)this.GetValue(DurationProperty);
            }
            set
            {
                this.SetValue(DurationProperty, value);
            }
        }
        private readonly Collection<InputBinding> inputBindings = new Collection<InputBinding>();
        private InputBinding inputBinding;
        public KeyGesture Gesture
        {
            set
            {
                if (this.inputBinding != null)
                    this.inputBindings.Clear();
                this.inputBinding = new InputBinding(new RelayCommand(p => this.LaunchSequence()), value);
                this.inputBindings.Add(this.inputBinding);
            }
        }
        public AnimationValueControleur(KeyGesture gesture) : this()
        {
            this.Gesture = gesture;
        }
        public AnimationValueControleur()
        {
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double), typeof(AnimationValueControleur), new PropertyMetadata(default(double), ValueChangedCallback));

        private static void ValueChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var _this = (AnimationValueControleur)dependencyObject;
            if (_this.ValueChanged != null)
            {
                _this.ValueChanged(_this, new EventArgs());
            }
        }

        public static readonly DependencyProperty EasingFunctionProperty = DependencyProperty.Register("EasingFunction", typeof(EasingFunctionBase), typeof(AnimationValueControleur), new PropertyMetadata(default(EasingFunctionBase)));

        public static readonly DependencyProperty CurveDirectionProperty = DependencyProperty.Register("CurveDirection", typeof(CurveDirection), typeof(AnimationValueControleur), new PropertyMetadata(default(CurveDirection)));

        internal void LaunchSequence()
        {
            if (this.CurveDirection == CurveDirection.Ascendant)
            {
                var anim = new DoubleAnimation(this.Value, 1, this.Duration, FillBehavior.Stop);
                anim.Completed += (sender, args) => this.Value = 1;
                if (this.EasingFunction != null)
                {
                    anim.EasingFunction = this.EasingFunction;
                }
                this.BeginAnimation(ValueProperty, anim);
            }
            else
            {
                var anim = new DoubleAnimation(this.Value, 0, this.Duration, FillBehavior.Stop);
                anim.Completed += (sender, args) => this.Value = 0;
                if (this.EasingFunction != null)
                {
                    anim.EasingFunction = this.EasingFunction;
                }
                this.BeginAnimation(ValueProperty, anim);
            }
        }

        public CurveDirection CurveDirection
        {
            get
            {
                return (CurveDirection)this.GetValue(CurveDirectionProperty);
            }
            set
            {
                this.SetValue(CurveDirectionProperty, value);
            }
        }

        public EasingFunctionBase EasingFunction
        {
            get
            {
                return (EasingFunctionBase)this.GetValue(EasingFunctionProperty);
            }
            set
            {
                this.SetValue(EasingFunctionProperty, value);
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

        protected override Freezable CreateInstanceCore()
        {
            return (Freezable)this;
        }

        public double Minimum
        {
            get
            {
                return 0;
            }
        }

        public double Maximum
        {
            get
            {
                return 1;
            }
        }

        public event EventHandler ValueChanged;
    }
}