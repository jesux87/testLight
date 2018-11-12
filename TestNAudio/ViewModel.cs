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
            InitCommand();

            InitBandCollection();
        }

        public IEnumerable<Equalizer.EqualizerBand> Bands
        {
            get
            {
                return bands;
            }
            set
            {
                bands = value;
            }
        }

        private void InitBandCollection()
        {
            Bands = new List<Equalizer.EqualizerBand>
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
                selectedFile = openFileDialog.FileName;
                reader = new AudioFileReader(selectedFile);
                equalizer = new Equalizer(reader, Bands);
                player = new WaveOutEvent();
                player.Init(equalizer, false);
                player.Play();
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
                return sourceProvider.WaveFormat;
            }
        }

        public Equalizer(ISampleProvider sourceProvider, IEnumerable<EqualizerBand> bands)
        {
            sourceProvider = sourceProvider;
            bands = bands;
            channels = sourceProvider.WaveFormat.Channels;
            bandCount = bands.Count();
            filters = new BiQuadFilter[channels, bandCount];
            CreateFilters();
        }

        private void CreateFilters()
        {
            var i = 0;
            foreach (var equalizerBand in bands)
            {
                for (int j = 0; j < channels; j++)
                {
                    if (filters[j, i] == null)
                    {
                        filters[j, i] = BiQuadFilter.PeakingEQ((float)sourceProvider.WaveFormat.SampleRate, (float)equalizerBand.Frequency, (float)equalizerBand.Bandwidth, (float)equalizerBand.Gain);
                    }
                    else
                    {
                        filters[j, i].SetPeakingEq((float)sourceProvider.WaveFormat.SampleRate, (float)equalizerBand.Frequency, (float)equalizerBand.Bandwidth, (float)equalizerBand.Gain);
                    }
                }

                i++;
            }
        }

        public void Update()
        {
            updated = true;
            CreateFilters();
        }

        public int Read(float[] buffer, int offset, int count)
        {
            int num = sourceProvider.Read(buffer, offset, count);
            if (updated)
            {
                CreateFilters();
                updated = false;
            }
            for (int i = 0; i < num; i++)
            {
                int num2 = i % channels;
                for (int j = 0; j < bandCount; j++)
                {
                    buffer[offset + i] = filters[num2, j].Transform(buffer[offset + i]);
                }
            }
            return num;
        }
    }
}
