using System;
using System.Xml.Serialization;

namespace TestNAudio.Model.XmlModel
{
    public class SingleLightFader : LightFader
    {
        [XmlAttribute]
        public int Level { get; set; }

        [XmlAttribute]
        public Guid IdSource { get; set; }
    }
}