using System.Collections.Generic;
using System.Xml.Serialization;

namespace TestNAudio.Model.XmlModel
{
    public class Sequence
    {
        [XmlAttribute]
        public string Name { get; set; }

        public List<AudioSequence> AudioSequences { get; set; }

        public List<LightSequence> LightSequences { get; set; }
    }
}