using UnityEngine;
using System.Collections;

public static class DataCore {

    public static class VolumeData
    {
        public static float musicVolume = 0.8f;
        public static float soundVoume = 0.4f;
    }

    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

}
