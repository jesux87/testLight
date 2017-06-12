using System;
using System.Xml.Serialization;

namespace TestNAudio.Model.XmlModel
{
    public class AudioFader
    {
        [XmlAttribute]
        public int Level { get; set; }

        [XmlAttribute]
        public float Value { get; set; }

        [XmlAttribute]
        public Guid IdSource { get; set; }
    }
}