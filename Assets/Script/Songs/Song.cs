using System;
using UnityEngine;

namespace Script.Songs{
    public interface ISong{
        String GetName();

        int GetDuration();

        AudioClip GetAudioClip();

        float GetBpm();

        float GetOffSet();
    }
}