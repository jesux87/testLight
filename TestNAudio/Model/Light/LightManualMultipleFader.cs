using System;
using System.Linq;

using Common;

using TestNAudio.ValueControl;

namespace TestNAudio.Model.Light
{
    public class LightManualMultipleFader : NotifyPropertyChangedObject, ILightController //: LightDecorator
    {
        private readonly FaderValueControleur valueControleur;

        private string name;

        public LightManualMultipleFader(FaderValueControleur valueControleur, ILightProvider[] lightProviders, string name = null)
            //: base(lightProvider)
        {
            this.name = name;
            this.valueControleur = valueControleur;
            this.LightProviders = lightProviders.Select(lp => new LightManualSimpleFader(valueControleur, lp)).ToArray();
            this.valueControleur.ValueChanged += (sender, args) => this.RaiseValueChanged();
        }

        protected void RaiseValueChanged()
        {
            if (this.ValueChanged != null)
            {
                this.ValueChanged(this, new EventArgs());
            }
            this.RaisePropertyChanged(() => this.Value);
        }


        public ILightProvider[] LightProviders { get; private set; }

        public FaderValueControleur ValueControleur
        {
            get
            {
                return this.valueControleur;
            }
        }

        public byte Value
        {
            get
            {
                return (byte)(this.ValueControleur.Value * 255);
            }
        }

        public byte Minimum
        {
            get { return 0; }
        }

        public byte Maximum
        {
            get { return 255; }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public event EventHandler ValueChanged;

    }
}