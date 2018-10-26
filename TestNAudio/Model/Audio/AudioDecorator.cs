namespace TestNAudio.Model.Audio
{
    using NAudio.Wave;

    public abstract class AudioDecorator : IAudioProvider
    {
        protected AudioDecorator(IAudioProvider component)
        {
            this.Component = component;
            this.Component.Parent = this;
        }

        public virtual WaveFormat WaveFormat
        {
            get
            {
                return this.Component.WaveFormat;
            }
        }

        public WaveChannel32 Channel
        {
            get
            {
                return this.Component.Channel;
            }
        }

        protected IAudioProvider Component { get; private set; }


        public int Read(byte[] buffer, int offset, int count)
        {
            return this.Parent != null ? this.Parent.Read(buffer, offset, count) : this.ReadInternal(buffer, offset, count);
        }

        public virtual int ReadInternal(byte[] buffer, int offset, int count)
        {
            return this.Component.ReadInternal(buffer, offset, count);
        }
        public IAudioProvider Parent { private get; set; }
    }

    public abstract class AudioDecorator<T> : IAudioProvider
    {
        protected AudioDecorator(IAudioProvider component)
        {
            this.Component = component;
            this.Component.Parent = this;
        }

        public virtual WaveFormat WaveFormat
        {
            get
            {
                return this.Component.WaveFormat;
            }
        }
        public WaveChannel32 Channel
        {
            get
            {
                return this.Component.Channel;
            }
        }

        protected IAudioProvider Component { get; private set; }
        public IAudioProvider Parent { private get; set; }

        public abstract T Maximum { get; }

        public abstract T Minimum { get; }

        public abstract T Value { get; set; }

        public int Read(byte[] buffer, int offset, int count)
        {
            return this.Parent != null ? this.Parent.Read(buffer, offset, count) : this.ReadInternal(buffer,offset,count);
        }

        public virtual int ReadInternal(byte[] buffer, int offset, int count)
        {
            return this.Component.ReadInternal(buffer, offset, count);
        }
    }

}