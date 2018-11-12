using TestNAudio.ValueControl;

namespace TestNAudio.Model.Light
{
    public class LightManualSimpleFader : LightDecorator
    {
        private readonly FaderValueControleur _valueControleur;

        public LightManualSimpleFader(FaderValueControleur valueControleur, ILightProvider lightProvider, string name = null)
            : base(lightProvider, name)
        {
            _valueControleur = valueControleur;
            _valueControleur.ValueChanged += (sender, args) => RaiseValueChanged();
        }

        public FaderValueControleur ValueControleur
        {
            get
            {
                return _valueControleur;
            }
        }

        public override byte Value
        {
            get
            {
                return (byte)(ValueControleur.Value * Component.Value);
            }
        }
    }
}