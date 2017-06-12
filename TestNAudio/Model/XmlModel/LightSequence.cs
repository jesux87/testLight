using System;
using System.Windows.Media.Animation;
using System.Xml.Serialization;

namespace TestNAudio.Model.XmlModel
{
    public class LightSequence
    {
        [XmlAttribute]
        public int Level { get; set; }

        [XmlAttribute]
        public Guid IdSource { get; set; }

        [XmlAttribute]
        public byte From { get; set; }

        [XmlAttribute]
        public byte To { get; set; }

        [XmlIgnore]
        public TimeSpan? BeginTime { get; set; }

        [XmlIgnore]
        public TimeSpan Duration { get; set; }

        [XmlAttribute("Duration")]
        public long SerialDuration
        {
            get
            {
                return this.Duration.Ticks;
            }
            set
            {
                this.Duration = new TimeSpan(value);
            }
        }

        [XmlAttribute("BeginTime")]
        public long SerialBeginTime
        {
            get
            {
                return this.BeginTime.HasValue ? this.BeginTime.Value.Ticks : 0;
            }
            set
            {
                this.BeginTime = value != 0 ? new TimeSpan(value) : (TimeSpan?)null;
            }
        }

        [XmlAttribute]
        public int CurvePower { get; set; }

        [XmlAttribute]
        public EasingMode EasingMode { get; set; }
    }
}