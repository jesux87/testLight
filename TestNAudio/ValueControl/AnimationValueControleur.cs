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
                return inputBindings;
            }
        }

        public ICollection<AnimationValueControleur> Controlleurs
        {
            get
            {
                return controlleurs;
            }
        }

        public KeyGesture Gesture
        {
            set
            {
                if (inputBinding != null)
                    inputBindings.Clear();
                inputBinding = new InputBinding(new RelayCommand(p => LaunchSequences()), value);
                inputBindings.Add(inputBinding);
            }
        }

        public AnimationsControleur(KeyGesture gesture) : this()
        {
            Gesture = gesture;
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
                return (Duration)GetValue(DurationProperty);
            }
            set
            {
                SetValue(DurationProperty, value);
            }
        }
        private readonly Collection<InputBinding> inputBindings = new Collection<InputBinding>();
        private InputBinding inputBinding;
        public KeyGesture Gesture
        {
            set
            {
                if (inputBinding != null)
                    inputBindings.Clear();
                inputBinding = new InputBinding(new RelayCommand(p => LaunchSequence()), value);
                inputBindings.Add(inputBinding);
            }
        }
        public AnimationValueControleur(KeyGesture gesture) : this()
        {
            Gesture = gesture;
        }
        public AnimationValueControleur()
        {
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double), typeof(AnimationValueControleur), new PropertyMetadata(default(double), ValueChangedCallback));

        private static void ValueChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var @this = (AnimationValueControleur)dependencyObject;
            if (@this.ValueChanged != null)
            {
                @this.ValueChanged(@this, new EventArgs());
            }
        }

        public static readonly DependencyProperty EasingFunctionProperty = DependencyProperty.Register("EasingFunction", typeof(EasingFunctionBase), typeof(AnimationValueControleur), new PropertyMetadata(default(EasingFunctionBase)));

        public static readonly DependencyProperty CurveDirectionProperty = DependencyProperty.Register("CurveDirection", typeof(CurveDirection), typeof(AnimationValueControleur), new PropertyMetadata(default(CurveDirection)));

        internal void LaunchSequence()
        {
            if (CurveDirection == CurveDirection.Ascendant)
            {
                var anim = new DoubleAnimation(Value, 1, Duration, FillBehavior.Stop);
                anim.Completed += (sender, args) => Value = 1;
                if (EasingFunction != null)
                {
                    anim.EasingFunction = EasingFunction;
                }
                BeginAnimation(ValueProperty, anim);
            }
            else
            {
                var anim = new DoubleAnimation(Value, 0, Duration, FillBehavior.Stop);
                anim.Completed += (sender, args) => Value = 0;
                if (EasingFunction != null)
                {
                    anim.EasingFunction = EasingFunction;
                }
                BeginAnimation(ValueProperty, anim);
            }
        }

        public CurveDirection CurveDirection
        {
            get
            {
                return (CurveDirection)GetValue(CurveDirectionProperty);
            }
            set
            {
                SetValue(CurveDirectionProperty, value);
            }
        }

        public EasingFunctionBase EasingFunction
        {
            get
            {
                return (EasingFunctionBase)GetValue(EasingFunctionProperty);
            }
            set
            {
                SetValue(EasingFunctionProperty, value);
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