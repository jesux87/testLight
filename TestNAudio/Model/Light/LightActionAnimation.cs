using System;
using System.Collections.Generic;
using System.Windows.Media.Animation;
using System.Xml.Serialization;

namespace TestNAudio.Model.Light
{
    public class LightActionAnimation : LightDecorator
    {
        private readonly AnimationValueControleur animControleur;

        public LightActionAnimation(AnimationValueControleur animControleur, ILightProvider lightProvider)
            : base(lightProvider)
        {
            this.animControleur = animControleur;
            this.AnimControleur.ValueChanged += (sender, args) => this.RaiseValueChanged();
        }

        public AnimationValueControleur AnimControleur
        {
            get
            {
                return this.animControleur;
            }
        }

        public override byte Value
        {
            get
            {
                return (byte)(this.AnimControleur.Value * this.Component.Value);
            }
        }
    }
}
