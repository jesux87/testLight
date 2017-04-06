using System;

namespace TestNAudio.Model.Light
{
    public interface ILightProvider : ILightController
    {
        int Address { get; }
    }
    public interface ILightController
    {
        event EventHandler ValueChanged;

        byte Value { get; }

        byte Minimum { get; }

        byte Maximum { get; }

        string Name { get; }
    }
}