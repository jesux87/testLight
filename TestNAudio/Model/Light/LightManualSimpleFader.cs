using TestNAudio.ValueControl;

namespace TestNAudio.Model.Light
{
    public class LightManualSimpleFader : LightDecorator
    {
        private readonly FaderValueControleur valueControleur;

        public LightManualSimpleFader(FaderValueControleur valueControleur, ILightProvider lightProvider, string name = null)
            : base(lightProvider, name)
        {
            this.valueControleur = valueControleur;
            this.valueControleur.ValueChanged += (sender, args) => this.RaiseValueChanged();
        }

        public FaderValueControleur ValueControleur
        {
            get
            {
                return this.valueControleur;
            }
        }

        public override byte Value
        {
            get
            {
                return (byte)(this.ValueControleur.Value * this.Component.Value);
            }
        }
    }
}