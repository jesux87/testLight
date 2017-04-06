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
            this.cultureInfo = CultureInfo.GetCultureInfo(cultureInfoName);
        }

        [Obsolete("merci d'utiliser EnumDescriptionAttribute(string cultureInfoName) ansi que les propriétés en dynamique")]
        public EnumDescriptionAttribute(string cultureInfoName, string fullDescription = null, string shortDescription = null, string litteralDescription = null)
            : this(cultureInfoName)
        {
            this.fullDescription = fullDescription;
            this.shortDescription = shortDescription;
            this.litteralDescription = litteralDescription;
        }

        /// <summary>
        /// Affichage littéral (conseillé 10 caractères)
        /// </summary>
        public string LitteralDescription
        {
            get
            {
                return this.litteralDescription;
            }

            set
            {
                this.litteralDescription = value;
            }
        }

        /// <summary>
        /// Description courte pour tailles de zones de texte contraintes (conseillé max 5 caractères)
        /// </summary>
        public string ShortDescription
        {
            get
            {
                return this.shortDescription;
            }

            set
            {
                this.shortDescription = value;
            }
        }

        /// <summary>
        /// Phrase de description complète
        /// </summary>
        public string FullDescription
        {
            get
            {
                return this.fullDescription;
            }

            set
            {
                this.fullDescription = value;
            }
        }

        /// <summary>
        /// Caractère pour icone de correspondance
        /// </summary>
        public char Icon
        {
            get
            {
                return this.icon;
            }

            set
            {
                this.icon = value;
            }
        }

        internal CultureInfo CultureInfo
        {
            get
            {
                return this.cultureInfo;
            }
        }
    }
}
