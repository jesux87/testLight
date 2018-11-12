using System;

namespace TestNAudio.Model.Light
{
    public class LightProvider : ILightProvider
    {
        public LightProvider(int address)
        {
            Address = address;
        }

        public byte Value
        {
            get { return (byte)(BlackOut? 0 : 255); }
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
                return Address.ToString();
            }
        }
    }

    public class LightRgbProvider
    {
        public LightRgbProvider(int adressR, int adressG, int adressB)
        {
            Red = new LightProvider(adressR);
            Green = new LightProvider(adressG);
            Blue = new LightProvider(adressB);
        }

        public ILightProvider Red { get; private set; }

        public ILightProvider Green { get; private set; }

        public ILightProvider Blue { get; private set; }
    }
}