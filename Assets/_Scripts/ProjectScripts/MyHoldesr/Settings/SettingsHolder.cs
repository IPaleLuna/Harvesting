using System.IO;
using PaleLuna.Architecture.Services;
using PaleLuna.JSONUtility;
using UnityEngine;

namespace GameSettings
{
    public class SettingsHolder : IService
    {
        private const string SETTINGS_FILE = "settings.json";
        private const string ROOT_FOLDER = "Resources";
        
        public readonly string SETTINGS_PATH;
        
        private Settings _settings;
        public Settings settings => _settings;

        public SettingsHolder()
        {
            SETTINGS_PATH = Path.Combine(Application.dataPath, ROOT_FOLDER, SETTINGS_FILE);
            Debug.Log(SETTINGS_FILE);
        }

        public void SetAudioSettings(AudioSettings audioSettings)
        {
            _settings.audio.Copy(audioSettings);
        }

        public void ApplyChanges()
        {
            JSONUtility.WriteToFile(SETTINGS_PATH, _settings);
        }

        public bool LoadSettings()
        {
            bool fileExist = JSONUtility.TryReadFromFile(SETTINGS_PATH, out Settings settings);
            _settings =  fileExist ? settings : new Settings();
            
            return fileExist;
        }

    }
}


