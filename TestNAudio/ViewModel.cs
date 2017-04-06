using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;

using Microsoft.Win32;

using NAudio.Dsp;
using NAudio.Wave;

namespace TestNAudio
{
    public class ViewModel : NotifyPropertyChangedObject
    {
        private IEnumerable<Equalizer.EqualizerBand> bands;

        private Equalizer equalizer;
        private string selectedFile;
        private AudioFileReader reader;
        private WaveOutEvent player;
        public ViewModel()
        {
            this.InitCommand();

            this.InitBandCollection();
        }

        public IEnumerable<Equalizer.EqualizerBand> Bands
        {
            get
            {
                return this.bands;
            }
            set
            {
                this.bands = value;
            }
        }

        private void InitBandCollection()
        {
            this.Bands = new List<Equalizer.EqualizerBand>
                             {
                                 new Equalizer.EqualizerBand
                                     {
                                         Bandwidth = 0.8f,
                                         Frequency = 100f,
                                         Gain = 0f
                                     },
                                 new Equalizer.EqualizerBand
                                     {
                                         Bandwidth = 0.8f,
                                         Frequency = 200f,
                                         Gain = 0f
                                     },
                                 new Equalizer.EqualizerBand
                                     {
                                         Bandwidth = 0.8f,
                                         Frequency = 400f,
                                         Gain = 0f
                                     },
                                 new Equalizer.EqualizerBand
                                     {
                                         Bandwidth = 0.8f,
                                         Frequency = 800f,
                                         Gain = 0f
                                     },
                                 new Equalizer.EqualizerBand
                                     {
                                         Bandwidth = 0.8f,
                                         Frequency = 1200f,
                                         Gain = 0f
                                     },
                                 new Equalizer.EqualizerBand
                                     {
                                         Bandwidth = 0.8f,
                                         Frequency = 2400f,
                                         Gain = 0f
                                     },
                                 new Equalizer.EqualizerBand
                                     {
                                         Bandwidth = 0.8f,
                                         Frequency = 4800f,
                                         Gain = 0f
                                     },
                                 new Equalizer.EqualizerBand
                                     {
                                         Bandwidth = 0.8f,
                                         Frequency = 9600f,
                                         Gain = 0f
                                     }
                             };
        }

        private void InitCommand()
        {
            
        }
        
        public void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All Supported Files (*.wav;*.mp3)|*.wav;*.mp3|All Files (*.*)|*.*";
            bool? flag = openFileDialog.ShowDialog();
            if (flag.HasValue && flag.Value)
            {
                this.selectedFile = openFileDialog.FileName;
                this.reader = new AudioFileReader(this.selectedFile);
                this.equalizer = new Equalizer(this.reader, this.Bands);
                this.player = new WaveOutEvent();
                this.player.Init(this.equalizer, false);
                this.player.Play();
            }
        }
    }

    public class Equalizer : ISampleProvider
    {
        public class EqualizerBand
        {
            public float Frequency
            {
                get;
                set;
            }

            public float Gain
            {
                get;
                set;
            }

            public float Bandwidth
            {
                get;
                set;
            }
        }

        private readonly ISampleProvider sourceProvider;

        private readonly IEnumerable<EqualizerBand> bands;

        private readonly BiQuadFilter[,] filters;

        private readonly int channels;

        private readonly int bandCount;

        private bool updated;

        public WaveFormat WaveFormat
        {
            get
            {
                return this.sourceProvider.WaveFormat;
            }
        }

        public Equalizer(ISampleProvider sourceProvider, IEnumerable<EqualizerBand> bands)
        {
            this.sourceProvider = sourceProvider;
            this.bands = bands;
            this.channels = sourceProvider.WaveFormat.Channels;
            this.bandCount = this.bands.Count();
            this.filters = new BiQuadFilter[this.channels, this.bandCount];
            this.CreateFilters();
        }

        private void CreateFilters()
        {
            var i = 0;
            foreach (var equalizerBand in this.bands)
            {
                for (int j = 0; j < this.channels; j++)
                {
                    if (this.filters[j, i] == null)
                    {
                        this.filters[j, i] = BiQuadFilter.PeakingEQ((float)this.sourceProvider.WaveFormat.SampleRate, (float)equalizerBand.Frequency, (float)equalizerBand.Bandwidth, (float)equalizerBand.Gain);
                    }
                    else
                    {
                        this.filters[j, i].SetPeakingEq((float)this.sourceProvider.WaveFormat.SampleRate, (float)equalizerBand.Frequency, (float)equalizerBand.Bandwidth, (float)equalizerBand.Gain);
                    }
                }

                i++;
            }
        }

        public void Update()
        {
            this.updated = true;
            this.CreateFilters();
        }

        public int Read(float[] buffer, int offset, int count)
        {
            int num = this.sourceProvider.Read(buffer, offset, count);
            if (this.updated)
            {
                this.CreateFilters();
                this.updated = false;
            }
            for (int i = 0; i < num; i++)
            {
                int num2 = i % this.channels;
                for (int j = 0; j < this.bandCount; j++)
                {
                    buffer[offset + i] = this.filters[num2, j].Transform(buffer[offset + i]);
                }
            }
            return num;
        }
    }
}
