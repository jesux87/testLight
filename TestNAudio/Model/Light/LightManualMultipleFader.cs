using System;
using System.Linq;

using Common;

using TestNAudio.ValueControl;

namespace TestNAudio.Model.Light
{
    public class LightManualMultipleFader : NotifyPropertyChangedObject, ILightController //: LightDecorator
    {
        private readonly FaderValueControleur _valueControleur;

        private string _name;

        public LightManualMultipleFader(FaderValueControleur valueControleur, ILightProvider[] lightProviders, string name = null)
            //: base(lightProvider)
        {
            _name = name;
            _valueControleur = valueControleur;
            LightProviders = lightProviders.Select(lp => new LightManualSimpleFader(valueControleur, lp)).ToArray();
            valueControleur.ValueChanged += (sender, args) => RaiseValueChanged();
        }

        protected void RaiseValueChanged()
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, new EventArgs());
            }
            RaisePropertyChanged(() => Value);
        }


        public ILightProvider[] LightProviders { get; private set; }

        public FaderValueControleur ValueControleur
        {
            get
            {
                return _valueControleur;
            }
        }

        public byte Value
        {
            get
            {
                return (byte)(ValueControleur.Value * 255);
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
                return _name;
            }
        }

        public event EventHandler ValueChanged;

    }
}