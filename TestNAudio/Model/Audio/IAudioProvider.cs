using System;

using NAudio.Wave;

namespace TestNAudio.Model.Audio
{
    public interface IAudioProvider : IWaveProvider
    {
        WaveChannel32 Channel { get; }

        IAudioProvider Parent { set; }

        int ReadInternal(byte[] buffer, int offset, int count);
    }
}