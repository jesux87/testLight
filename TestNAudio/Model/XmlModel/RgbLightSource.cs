using System.Xml.Serialization;

namespace TestNAudio.Model.XmlModel
{
    public class RgbLightSource : LightSource
    {
        [XmlAttribute]
        public ushort RedChannel { get; set; }

        [XmlAttribute]
        public ushort GreenChannel { get; set; }

        [XmlAttribute]
        public ushort BlueChannel { get; set; }

    }
}