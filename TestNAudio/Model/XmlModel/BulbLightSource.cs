using System.Windows.Media;
using System.Xml.Serialization;

namespace TestNAudio.Model.XmlModel
{
    public class BulbLightSource : LightSource
    {
        [XmlAttribute]
        public ushort Channel { get; set; }

        [XmlIgnore]
        public Color BulbColor { get; set; }

        [XmlAttribute("BulbColor")]
        public string SerialBulbColor {
            get
            {
                return BulbColor.ToString();
            } set
        {
            BulbColor = (Color)ColorConverter.ConvertFromString(value);
        } }
    }
}