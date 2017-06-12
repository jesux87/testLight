using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

using Common;

using NAudio.Wave;

using NAudioWpfDemo;

using WpfTools;

namespace TestNAudio.Model.Audio
{
    public class AudioProvider : NotifyPropertyChangedObject, IAudioProvider
    {
        private readonly WaveChannel32 channel;

        public AudioProvider(WaveStream stream)
        {
            this.OutPutWave = new WaveOutEvent();
            this.channel = new WaveChannel32(stream);
            this.OutPutWave.Init(this);
            
            this.playPauseCommand = new RelayCommand(p =>
                                        {
                                            this.playing = !playing;
                                            if (playing)
                                                this.OutPutWave.Play();
                                            else
                                            {
                                                this.OutPutWave.Pause();
                                            }
                                        });
        }

        public WaveOutEvent OutPutWave { get; private set; }

        private readonly RelayCommand playPauseCommand;

        private bool playing;

        public WaveChannel32 Channel
        {
            get
            {
                return this.channel;
            }
        }

        void truc()
        {
            //this.channel.ReadAsync()
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            return this.Parent != null ? this.Parent.Read(buffer, offset, count) : this.ReadInternal(buffer, offset, count);
        }

        public int ReadInternal(byte[] buffer, int offset, int count)
        {
            return this.channel.Read(buffer, offset, count);
        }

        public WaveFormat WaveFormat
        {
            get { return this.channel.WaveFormat; }
        }
        
        public ICommand PlayPauseCommand
        {
            get
            {
                return this.playPauseCommand;
            }
        }

        public bool Playing
        {
            get
            {
                return this.playing;
            }
        }

        public IAudioProvider Parent
        {
            private get; set;
        }
    }
    public class AudioVolume : AudioDecorator<float>
    {
        private const float MAX_VOLUME = 1;
        private const float MIN_VOLUME = 0;
        
        public AudioVolume(IAudioProvider component) : base(component)
        {
            
        }
        
        public override float Maximum
        {
            get
            {
                return MAX_VOLUME;
            }
        }

        public override float Minimum
        {
            get
            {
                return MIN_VOLUME;
            }
        }

        public override float Value
        {
            get
            {
                return this.Component.Channel.Volume;
            }
            set
            {
                this.Component.Channel.Volume = Math.Max(MIN_VOLUME, Math.Min(MAX_VOLUME, value));
            }
        }
    }

    public class AudioPan : AudioDecorator<float>
    {
        private const float MIN_MAX_PAN = 1f;
        
        public AudioPan(IAudioProvider component): base(component)
        {
        }

        public override float Value
        {
            get
            {
                return this.Component.Channel.Pan;
            }
            set
            {
                this.Component.Channel.Pan = Math.Max(-MIN_MAX_PAN, Math.Min(MIN_MAX_PAN, value));
            }
        }

        public override float Maximum
        {
            get
            {
                return MIN_MAX_PAN;
            }
        }

        public override float Minimum
        {
            get
            {
                return -MIN_MAX_PAN;
            }
        }
    }
    //public class AudioProvider : IAudioProvider
    //{
    //    private AudioFileReader reader;
    //    public AudioProvider(string fileName)
    //    {
    //        this.reader = new AudioFileReader(fileName);
    //    }

    //    public float Volume { get; set; }

    //    public int Read(float[] buffer, int offset, int count)
    //    {
    //        return this.reader.Read(buffer, offset, count);
    //    }

    //    public WaveFormat WaveFormat
    //    {
    //        get
    //        {
    //            return this.reader.WaveFormat;
    //        }
    //    }
    //}

    //public class MultiAudioProvider : IAudioProvider
    //{
    //    private MultiplexingWaveProvider provider;
    //    public MultiAudioProvider(string[] fileName)
    //    {
    //        var fs = fileName.Select(f => new Mp3FileReader(f)).ToArray();
    //        this.provider = new MultiplexingWaveProvider(fs, 2);
    //    }

    //    public float Volume { get; set; }

    //    public int Read(byte[] buffer, int offset, int count)
    //    {
    //        return this.provider.Read(buffer, offset, count);
    //    }

    //    public WaveFormat WaveFormat
    //    {
    //        get
    //        {
    //            return this.provider.WaveFormat;
    //        }
    //    }
    //}
}