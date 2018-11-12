using System;
using System.Globalization;

namespace Common.EnumDescription
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class EnumDescriptionAttribute : Attribute
    {
        private readonly CultureInfo cultureInfo;

        private string litteralDescription = string.Empty;

        private string shortDescription = string.Empty;

        private string fullDescription = string.Empty;

        private char icon = '\0';

        public EnumDescriptionAttribute(string cultureInfoName)
        {
            cultureInfo = CultureInfo.GetCultureInfo(cultureInfoName);
        }

        [Obsolete("merci d'utiliser EnumDescriptionAttribute(string cultureInfoName) ansi que les propriétés en dynamique")]
        public EnumDescriptionAttribute(string cultureInfoName, string fullDescription = null, string shortDescription = null, string litteralDescription = null)
            : this(cultureInfoName)
        {
            fullDescription = fullDescription;
            shortDescription = shortDescription;
            litteralDescription = litteralDescription;
        }

        /// <summary>
        /// Affichage littéral (conseillé 10 caractères)
        /// </summary>
        public string LitteralDescription
        {
            get
            {
                return litteralDescription;
            }

            set
            {
                litteralDescription = value;
            }
        }

        /// <summary>
        /// Description courte pour tailles de zones de texte contraintes (conseillé max 5 caractères)
        /// </summary>
        public string ShortDescription
        {
            get
            {
                return shortDescription;
            }

            set
            {
                shortDescription = value;
            }
        }

        /// <summary>
        /// Phrase de description complète
        /// </summary>
        public string FullDescription
        {
            get
            {
                return fullDescription;
            }

            set
            {
                fullDescription = value;
            }
        }

        /// <summary>
        /// Caractère pour icone de correspondance
        /// </summary>
        public char Icon
        {
            get
            {
                return icon;
            }

            set
            {
                icon = value;
            }
        }

        internal CultureInfo CultureInfo
        {
            get
            {
                return cultureInfo;
            }
        }
    }
}
