using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Remoting.Messaging;

using Common;

using NAudio.Dsp;

using TestNAudio.ValueControl;

namespace TestNAudio.Model.Audio
{
    public class AudioEqualizer : AudioDecorator
    {
        public class EqualizerBand : NotifyPropertyChangedObject
        {
            private readonly BiQuadFilter filter;

            private readonly float formatSampleRate;

            private readonly float frequency;

            private readonly float qFactor;

            private FilterType filterType;

            private FaderValueControleur valueControleur;

            public EqualizerBand(float formatSampleRate, float frequency, float qFactor, FilterType filterType = FilterType.Peaking)
            {
                this.formatSampleRate = formatSampleRate;
                this.frequency = frequency;
                this.qFactor = qFactor;
                this.filterType = filterType;
                this.valueControleur = new FaderValueControleur(new KeyboardControlMapping()) { Maximum = 30, Minimum = -30 };
                this.ValueControleur.ValueChanged += (sender, args) =>
                    {
                        this.Filter.SetPeakingEq(this.formatSampleRate, this.frequency, this.qFactor, this.Gain);
                        this.RaisePropertyChanged(() => this.Gain);
                    };
                //switch (this.filterType)
                //{
                //    case FilterType.All:
                //        this.filter = BiQuadFilter.NotchFilter(this.formatSampleRate, this.frequency, this.qFactor);
                //        break;
                //    case FilterType.HighFilter:
                //        this.filter = BiQuadFilter.HighPassFilter(this.formatSampleRate, this.frequency, qFactor);
                //        break;
                //    case FilterType.LowFilter:
                //        this.filter = BiQuadFilter.LowPassFilter(this.formatSampleRate, this.frequency, qFactor);
                //        break;
                //    default:
                //        this.filter = BiQuadFilter.PeakingEQ(this.formatSampleRate, this.frequency, this.qFactor, this.Gain);
                //        break;
                //}


                this.filter = BiQuadFilter.PeakingEQ(this.formatSampleRate, this.frequency, this.qFactor, this.Gain);
            }

            public float Frequency
            {
                get
                {
                    return this.frequency;
                }
            }

            public float Gain
            {
                get
                {
                    return (float)this.ValueControleur.Value;
                }
            }

            public float Bandwidth
            {
                get
                {
                    return this.qFactor;
                }
            }

            public BiQuadFilter Filter
            {
                get
                {
                    return this.filter;
                }
            }

            public FaderValueControleur ValueControleur
            {
                get
                {
                    return this.valueControleur;
                }
            }
        }

        private List<EqualizerBand> bands;

        public AudioEqualizer(IAudioProvider component)
            : base(component)
        {
            this.InitBands();
        }

        public IEnumerable<EqualizerBand> Bands
        {
            get
            {
                return this.bands;
            }
        }
        
        private void InitBands()
        {
            // original 8 bands
            this.bands = new List<EqualizerBand>
                             {
                                 new EqualizerBand(this.Component.WaveFormat.SampleRate, 0100f, 0.8f),
                                 new EqualizerBand(this.Component.WaveFormat.SampleRate, 0200f, 0.8f),
                                 new EqualizerBand(this.Component.WaveFormat.SampleRate, 0400f, 0.8f),
                                 new EqualizerBand(this.Component.WaveFormat.SampleRate, 0800f, 0.8f),
                                 new EqualizerBand(this.Component.WaveFormat.SampleRate, 1200f, 0.8f),
                                 new EqualizerBand(this.Component.WaveFormat.SampleRate, 2400f, 0.8f),
                                 new EqualizerBand(this.Component.WaveFormat.SampleRate, 4800f, 0.8f),
                                 new EqualizerBand(this.Component.WaveFormat.SampleRate, 9600f, 0.8f)
                             };

            // bande précision 1 octave
            //this.bands = new List<EqualizerBand>
            //                 {
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 00032f, 1.4142135f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 00064f, 1.4142135f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 00125f, 1.4142135f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 00250f, 1.4142135f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 00500f, 1.4142135f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 01000f, 1.4142135f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 02000f, 1.4142135f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 04000f, 1.4142135f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 08000f, 1.4142135f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 16000f, 1.4142135f)
            //                 };

            // bande precision 1/3 octave
            //this.bands = new List<EqualizerBand>
            //                 {
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 00012.5f, 4.318477f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 00016.0f, 4.318477f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 00020.0f, 4.318477f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 00025.0f, 4.318477f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 00031.5f, 4.318477f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 00040.0f, 4.318477f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 00050.0f, 4.318477f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 00063.5f, 4.318477f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 00080.0f, 4.318477f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 00100.0f, 4.318477f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 00125.0f, 4.318477f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 00160.0f, 4.318477f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 00200.0f, 4.318477f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 00250.0f, 4.318477f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 00315.0f, 4.318477f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 00400.0f, 4.318477f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 00500.0f, 4.318477f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 00800.0f, 4.318477f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 01000.0f, 4.318477f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 01250.0f, 4.318477f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 01600.0f, 4.318477f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 02000.0f, 4.318477f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 02500.0f, 4.318477f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 03150.0f, 4.318477f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 04000.0f, 4.318477f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 05000.0f, 4.318477f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 06300.0f, 4.318477f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 08000.0f, 4.318477f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 10000.0f, 4.318477f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 12500.0f, 4.318477f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 16000.0f, 4.318477f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 20000.0f, 4.318477f)
            //                 };

            // bande low/mid/high
            //this.bands = new List<EqualizerBand>
            //                 {
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 00100f, 0.625f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 01000f, 0.625f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 10000f, 0.625f)
            //                 };

            //this.bands = new List<EqualizerBand>
            //                 {
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 40f, 1f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 160f, 1f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 640f, 1f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 2560f, 1f),
            //                     new EqualizerBand(this.Component.WaveFormat.SampleRate, 10240, 1f),
            //                 };
        }

        public override int ReadInternal(byte[] buffer, int offset, int count)
        {
            var num = base.ReadInternal(buffer, offset, count);

            for (int i = 0; i < num; i++)
            {
                foreach (var band in this.Bands)
                {
                    buffer[offset + i] = (byte)band.Filter.Transform(buffer[offset + i]);
                }
            }

            return num;
        }
    }
}