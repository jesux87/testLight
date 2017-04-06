using System;
using System.Collections;

namespace TestNAudio
{
    public interface IValueControleur
    {
        ICollection InputBindings { get; }

        double Value { get; set; }

        event EventHandler ValueChanged;

        double Minimum { get; }

        double Maximum { get; }
    }
}
