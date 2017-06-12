using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

using TestNAudio.Model.Audio;
using TestNAudio.Model.Light;

using WpfTools;

namespace TestNAudio.Model.Sequence
{
    public class Sequencer : Storyboard
    {
        private readonly Collection<InputBinding> inputBindings = new Collection<InputBinding>();

        private readonly KeyGesture gesture;
        
        public ICollection InputBindings
        {
            get
            {
                return this.inputBindings;
            }
        }
        
        public Sequencer(KeyGesture gesture)
        {
            this.gesture = gesture;
            this.InitCommands();
        }

        private void InitCommands()
        {
            this.inputBindings.Add(new InputBinding(new RelayCommand(p => this.LaunchSequences()), this.gesture));
        }

        private void LaunchSequences()
        {
            ;
        }
    }

    public class AudioVolumeSequence : AnimationTimeline
    {
        private readonly AudioVolume volumeProvider;

        public AudioVolumeSequence(AudioVolume volumeProvider)
        {
            this.volumeProvider = volumeProvider;
            this.Changed += OnChanged;
        }

        private void OnChanged(object sender, EventArgs eventArgs)
        {
            ;
        }

        private void truc()
        {
            //Storyboard.SetTarget(this, this.volumeProvider);
            //Storyboard.SetTargetProperty(this,);

        }

        protected override Freezable CreateInstanceCore()
        {
            return (Freezable)new AudioVolumeSequence(this.volumeProvider);
        }

        public override Type TargetPropertyType
        {
            get
            {
                return typeof(double);
            }
        }
    }

    //public class LightSequence : Animatable, ILightController
    //{
    //    private readonly ILightProvider provider;

    //    public static DependencyProperty AnimatedValueProperty = DependencyProperty.Register("AnimatedValue", typeof(byte), typeof(LightSequence), new PropertyMetadata((o, args) =>
    //                                                                                                                                                                        {
    //                                                                                                                                                                            var ls = (LightSequence)o;
    //                                                                                                                                                                            ls.ValueChanged?.Invoke(ls, EventArgs.Empty);
    //                                                                                                                                                                        }));

    //    public byte AnimatedValue
    //    {
    //        get
    //        {
    //            return (byte)this.GetValue(AnimatedValueProperty);
    //        }
    //        set
    //        {
    //            this.SetValue(AnimatedValueProperty, value);
    //        }
    //    }

    //    public byte Minimum
    //    {
    //        get
    //        {
    //            return this.provider.Minimum;
    //        }
    //    }

    //    public byte Maximum
    //    {
    //        get
    //        {
    //            return this.provider.Maximum;
    //        }
    //    }

    //    public string Name
    //    {
    //        get
    //        {
    //            return this.provider.Name;
    //        }
    //    }

    //    public byte Value
    //    {
    //        get
    //        {
    //            throw new NotImplementedException();
    //        }
    //    }

    //    public event EventHandler ValueChanged;

    //    public LightSequence(ILightProvider provider)
    //    {
    //        this.provider = provider;
    //        this.Changed += OnChanged;
    //    }

    //    private void OnChanged(object sender, EventArgs eventArgs)
    //    {
    //        ;
    //    }

    //    private void truc()
    //    {
    //        //Storyboard.SetTarget(this, this.volumeProvider);
    //        //Storyboard.SetTargetProperty(this,);

    //    }

    //    protected override Freezable CreateInstanceCore()
    //    {
    //        return (Freezable)new LightSequence(this.provider);
    //    }
        
    //}
}
