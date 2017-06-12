using System;

namespace TestNAudio.Model.Light
{
    public class LightProvider : ILightProvider
    {
        public LightProvider(int address)
        {
            this.Address = address;
        }

        public byte Value
        {
            get { return (byte)(this.BlackOut? 0 : 255); }
        }

        public bool BlackOut { get; set; }

        public byte Minimum
        {
            get { return 0; }
        }

        public byte Maximum
        {
            get { return 255; }
        }

        public event EventHandler ValueChanged;

        public int Level
        {
            get
            {
                return 0;
            }
        }

        public int Address { get; private set; }

        public string Name
        {
            get
            {
                return this.Address.ToString();
            }
        }
    }

    public class LightRgbProvider
    {
        public LightRgbProvider(int adressR, int adressG, int adressB)
        {
            this.Red = new LightProvider(adressR);
            this.Green = new LightProvider(adressG);
            this.Blue = new LightProvider(adressB);
        }

        public ILightProvider Red { get; private set; }

        public ILightProvider Green { get; private set; }

        public ILightProvider Blue { get; private set; }
    }
}