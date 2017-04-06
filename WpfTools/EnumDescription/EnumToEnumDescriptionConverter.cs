using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Common.EnumDescription
{
    public class EnumToEnumDescriptionConverter : IValueConverter
    {
        public EnumTypeDescription TypeDescription { private get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            
            var memberInfo = value.GetType().GetMember(value.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(EnumDescriptionAttribute), false).Cast<EnumDescriptionAttribute>();

            var attribute = attributes.FirstOrDefault(att => att.CultureInfo.Equals(culture));
            if (attribute == null)
            {
                return string.Empty;
            }

            if (this.TypeDescription == EnumTypeDescription.Auto)
            {
                var autoLimit = parameter as int?;
                if (autoLimit.HasValue)
                {
                    return this.AutoConvert(attribute, autoLimit.Value);
                }

                return this.ManualConvert(EnumTypeDescription.Litteral, attribute);
            }

            return this.ManualConvert(this.TypeDescription, attribute);
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException();
        }

        private object AutoConvert(EnumDescriptionAttribute attribute, int p)
        {
            if (!string.IsNullOrEmpty(attribute.FullDescription) && attribute.FullDescription.Length <= p)
            {
                return attribute.FullDescription;
            }

            if (!string.IsNullOrEmpty(attribute.LitteralDescription) && attribute.LitteralDescription.Length <= p)
            {
                return attribute.LitteralDescription;
            }

            return attribute.ShortDescription;
        }

        private object ManualConvert(EnumTypeDescription typeDesc, EnumDescriptionAttribute attribute)
        {
            switch (typeDesc)
            {
                case EnumTypeDescription.Litteral:
                    return attribute.LitteralDescription;
                case EnumTypeDescription.Full:
                    return attribute.FullDescription;
                case EnumTypeDescription.Icon:
                    return attribute.Icon;
                default:
                    return attribute.ShortDescription;
            }
        }
    }
}
