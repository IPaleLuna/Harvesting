using UnityEngine;

namespace PaleLuna.Utility
{
    public static class SoundConverter
    {
        public static float ConvertVolume(float volume)
        {
            return Mathf.Log10(volume) * 20;
        }
    }
}

