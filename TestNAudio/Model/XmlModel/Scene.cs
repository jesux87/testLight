using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace TestNAudio.Model.XmlModel
{
    public class Scene
    {
        public static bool TryGet(string filePath, out Scene scene)
        {
            scene = null;
            try
            {
                var serializer = new XmlSerializer(typeof(Scene));
                using (var ms = XmlReader.Create(filePath))
                {
                    scene = (Scene)serializer.Deserialize(ms);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static bool TrySave(Scene scene, string filePath)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(Scene));
                using (var ms = XmlWriter.Create(filePath, new XmlWriterSettings
                                                               {
                                                                   NamespaceHandling = NamespaceHandling.OmitDuplicates,
                                                                   Indent = true
                                                               }))
                {
                    serializer.Serialize(ms, scene);
                }
            }
            catch (Exception) { return false; }
            return true;
        }

        [XmlAttribute]
        public string Name { get; set; }

        public List<LightSource> LightSources { get; set; }

        public List<AudioSource> AudioSources { get; set; }

        public List<LightFader> LightFaders { get; set; }

        public List<AudioFader> AudioFaders { get; set; }

        public List<Sequence> Sequences { get; set; }
    }
}
