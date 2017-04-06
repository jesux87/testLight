using System.Windows.Media;

using TestNAudio.Model.Light;

namespace TestNAudio
{
    public class RgbProjector
    {
        private readonly ILightProvider redProvider;

        private readonly ILightProvider greenProvider;

        private readonly ILightProvider blueProvider;

        public RgbProjector(ILightProvider redProvider, ILightProvider greenProvider, ILightProvider blueProvider)
        {
            this.redProvider = redProvider;
            this.greenProvider = greenProvider;
            this.blueProvider = blueProvider;
        }

        public ILightProvider RedProvider
        {
            get
            {
                return this.redProvider;
            }
        }

        public ILightProvider GreenProvider
        {
            get
            {
                return this.greenProvider;
            }
        }

        public ILightProvider BlueProvider
        {
            get
            {
                return this.blueProvider;
            }
        }
    }
    public class BulbProjector
    {
        private readonly ILightProvider provider;

        private readonly Color bulbColor;

        public BulbProjector(ILightProvider provider, Color bulbColor)
        {
            this.provider = provider;
            this.bulbColor = bulbColor;
        }

        public BulbProjector(ILightProvider provider) : this(provider, Colors.White)
        {}

        public ILightProvider Provider
        {
            get
            {
                return this.provider;
            }
        }

        public Color BulbColor
        {
            get
            {
                return this.bulbColor;
            }
        }
    }
}