namespace TestNAudio.Model.Audio
{
    using System;
    using System.Windows.Input;
    using Common;
    using NAudio.Wave;
    using WpfTools;

    public class AudioProvider : NotifyPropertyChangedObject, IAudioProvider
    {
        private readonly WaveChannel32 channel;

        public AudioProvider(WaveStream stream)
        {
            OutPutWave = new WaveOutEvent();
            channel = new WaveChannel32(stream);
            OutPutWave.Init(this);
            
            playPauseCommand = new RelayCommand(p =>
                                        {
                                            playing = !playing;
                                            if (playing)
                                                OutPutWave.Play();
                                            else
                                                OutPutWave.Pause();
                                        });
        }

        public WaveOutEvent OutPutWave { get; private set; }

        private readonly RelayCommand playPauseCommand;

        private bool playing;

        public WaveChannel32 Channel
        {
            get
            {
                return channel;
            }
        }
        
        public int Read(byte[] buffer, int offset, int count)
        {
            return Parent != null ? Parent.Read(buffer, offset, count) : ReadInternal(buffer, offset, count);
        }

        public int ReadInternal(byte[] buffer, int offset, int count)
        {
            return channel.Read(buffer, offset, count);
        }

        public WaveFormat WaveFormat
        {
            get { return channel.WaveFormat; }
        }
        
        public ICommand PlayPauseCommand
        {
            get
            {
                return playPauseCommand;
            }
        }

        public bool Playing
        {
            get
            {
                return playing;
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
                return Component.Channel.Volume;
            }
            set
            {
                Component.Channel.Volume = Math.Max(MIN_VOLUME, Math.Min(MAX_VOLUME, value));
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
                return Component.Channel.Pan;
            }
            set
            {
                Component.Channel.Pan = Math.Max(-MIN_MAX_PAN, Math.Min(MIN_MAX_PAN, value));
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
    //        reader = new AudioFileReader(fileName);
    //    }

    //    public float Volume { get; set; }

    //    public int Read(float[] buffer, int offset, int count)
    //    {
    //        return reader.Read(buffer, offset, count);
    //    }

    //    public WaveFormat WaveFormat
    //    {
    //        get
    //        {
    //            return reader.WaveFormat;
    //        }
    //    }
    //}

    //public class MultiAudioProvider : IAudioProvider
    //{
    //    private MultiplexingWaveProvider provider;
    //    public MultiAudioProvider(string[] fileName)
    //    {
    //        var fs = fileName.Select(f => new Mp3FileReader(f)).ToArray();
    //        provider = new MultiplexingWaveProvider(fs, 2);
    //    }

    //    public float Volume { get; set; }

    //    public int Read(byte[] buffer, int offset, int count)
    //    {
    //        return provider.Read(buffer, offset, count);
    //    }

    //    public WaveFormat WaveFormat
    //    {
    //        get
    //        {
    //            return provider.WaveFormat;
    //        }
    //    }
    //}
}