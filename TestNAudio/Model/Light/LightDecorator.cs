using System;

using Common;

namespace TestNAudio.Model.Light
{
    public abstract class LightDecorator : NotifyPropertyChangedObject, ILightProvider
    {
        private string _name;

        protected ILightProvider Component { get; private set; }

        protected LightDecorator(ILightProvider component, string name = null)
        {
            _name = name;
            Component = component;
            Component.ValueChanged += (sender, args) => RaiseValueChanged();
        }

        protected void RaiseValueChanged()
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, new EventArgs());
            }
            RaisePropertyChanged(() => Value);
        }

        public virtual byte Value
        {
            get
            {
                return Component.Value;
            }
        }

        public virtual byte Minimum
        {
            get
            {
                return Component.Minimum;
            }
        }

        public virtual byte Maximum
        {
            get
            {
                return Component.Maximum;
            }
        }

        public virtual string Name
        {
            get
            {
                if (string.IsNullOrEmpty(_name))
                    return Address.ToString();
                return _name;
            }
            protected set
            {
                _name = value;
            }
        }

        public event EventHandler ValueChanged;

        public int Level
        {
            get
            {
                return Component.Level + 1;
            }
        }

        public int Address
        {
            get
            {
                return Component.Address;
            }
        }


    }
}