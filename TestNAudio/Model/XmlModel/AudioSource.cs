using System;
using System.Xml.Serialization;

namespace TestNAudio.Model.XmlModel
{
    public class AudioSource
    {
        [XmlAttribute]
        public string FilePath { get; set; }

        [XmlAttribute]
        public Guid Id { get; set; }
    }
}