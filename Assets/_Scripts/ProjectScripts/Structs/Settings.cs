using System;

namespace GameSettings
{
    [Serializable]
    public class Settings
    {
        private AudioSettings _audio;
        
        public AudioSettings audio => _audio;

        public Settings(AudioSettings audio)
        {
            this._audio = audio;
        }

        public Settings()
        {
            _audio = new AudioSettings();
        }
    }
}


