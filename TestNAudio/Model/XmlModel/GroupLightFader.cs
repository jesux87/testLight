using System.Collections.Generic;

namespace TestNAudio.Model.XmlModel
{
    public class GroupLightFader : LightFader
    {
        public List<SingleLightFader> LightFaders { get; set; }
    }
}