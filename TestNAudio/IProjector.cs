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
            redProvider = redProvider;
            greenProvider = greenProvider;
            blueProvider = blueProvider;
        }

        public ILightProvider RedProvider
        {
            get
            {
                return redProvider;
            }
        }

        public ILightProvider GreenProvider
        {
            get
            {
                return greenProvider;
            }
        }

        public ILightProvider BlueProvider
        {
            get
            {
                return blueProvider;
            }
        }
    }
    public class BulbProjector
    {
        private readonly ILightProvider provider;

        private readonly Color bulbColor;

        public BulbProjector(ILightProvider provider, Color bulbColor)
        {
            provider = provider;
            bulbColor = bulbColor;
        }

        public BulbProjector(ILightProvider provider) : this(provider, Colors.White)
        {}

        public ILightProvider Provider
        {
            get
            {
                return provider;
            }
        }

        public Color BulbColor
        {
            get
            {
                return bulbColor;
            }
        }
    }
}