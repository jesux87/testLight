using System;

using Common;

namespace TestNAudio.Model.Light
{
    public abstract class LightDecorator : NotifyPropertyChangedObject, ILightProvider
    {
        private string name;

        protected ILightProvider Component { get; private set; }

        protected LightDecorator(ILightProvider component, string name = null)
        {
            this.name = name;
            this.Component = component;
            this.Component.ValueChanged += (sender, args) => this.RaiseValueChanged();
        }

        protected void RaiseValueChanged()
        {
            if (this.ValueChanged != null)
            {
                this.ValueChanged(this, new EventArgs());
            }
            this.RaisePropertyChanged(() => this.Value);
        }

        public virtual byte Value
        {
            get
            {
                return this.Component.Value;
            }
        }

        public virtual byte Minimum
        {
            get
            {
                return this.Component.Minimum;
            }
        }

        public virtual byte Maximum
        {
            get
            {
                return this.Component.Maximum;
            }
        }

        public virtual string Name
        {
            get
            {
                if (string.IsNullOrEmpty(this.name))
                    return this.Address.ToString();
                return this.name;
            }
            protected set
            {
                this.name = value;
            }
        }

        public event EventHandler ValueChanged;

        public int Level
        {
            get
            {
                return this.Component.Level + 1;
            }
        }

        public int Address
        {
            get
            {
                return this.Component.Address;
            }
        }


    }
}