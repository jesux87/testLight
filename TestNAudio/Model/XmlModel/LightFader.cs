using System.Xml.Serialization;

namespace TestNAudio.Model.XmlModel
{
    [XmlInclude(typeof(SingleLightFader))]
    [XmlInclude(typeof(GroupLightFader))]
    public abstract class LightFader
    {
        [XmlAttribute]
        public byte Value { get; set; }
    }
}