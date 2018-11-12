using System;
using System.Windows.Media.Animation;
using System.Xml.Serialization;

namespace TestNAudio.Model.XmlModel
{
    public class AudioSequence
    {
        [XmlAttribute]
        public int Level { get; set; }

        [XmlAttribute]
        public Guid IdSource { get; set; }

        [XmlAttribute]
        public float From { get; set; }

        [XmlAttribute]
        public float To { get; set; }

        [XmlIgnore]
        public TimeSpan? BeginTime { get; set; }

        [XmlIgnore]
        public TimeSpan Duration { get; set; }

        [XmlAttribute("Duration")]
        public long SerialDuration
        {
            get
            {
                return Duration.Ticks;
            }
            set
            {
                Duration = new TimeSpan(value);
            }
        }

        [XmlAttribute("BeginTime")]
        public long SerialBeginTime
        {
            get
            {
                return BeginTime.HasValue ? BeginTime.Value.Ticks : 0;
            }
            set
            {
                BeginTime = value != 0 ? new TimeSpan(value) : (TimeSpan?)null;
            }
        }

        [XmlAttribute]
        public int CurvePower { get; set; }

        [XmlAttribute]
        public EasingMode EasingMode { get; set; }
    }
}