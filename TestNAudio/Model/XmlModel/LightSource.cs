using System;
using System.Xml.Serialization;

namespace TestNAudio.Model.XmlModel
{
    [XmlInclude(typeof(BulbLightSource))]
    [XmlInclude(typeof(RgbLightSource))]
    public abstract class LightSource
    {
        [XmlAttribute]
        public Guid Id { get; set; }
    }
}