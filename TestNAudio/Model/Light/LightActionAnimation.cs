using System;
using System.Collections.Generic;
using System.Windows.Media.Animation;
using System.Xml.Serialization;

namespace TestNAudio.Model.Light
{
    public class LightActionAnimation : LightDecorator
    {
        private readonly AnimationValueControleur _animControleur;

        public LightActionAnimation(AnimationValueControleur animControleur, ILightProvider lightProvider)
            : base(lightProvider)
        {
            _animControleur = animControleur;
            AnimControleur.ValueChanged += (sender, args) => RaiseValueChanged();
        }

        public AnimationValueControleur AnimControleur
        {
            get
            {
                return _animControleur;
            }
        }

        public override byte Value
        {
            get
            {
                return (byte)(AnimControleur.Value * Component.Value);
            }
        }
    }
}
