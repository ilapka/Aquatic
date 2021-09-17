using UnityEngine;
using UnityEngine.Serialization;

namespace Data
{
    [CreateAssetMenu(fileName = "SaveSettingsData",menuName = "Aquatic/Save Settings Data")]
    public class SavingSettings : ScriptableObject
    {
        public string[] pathToFile;
        public string[] pathToEncryptFile;
        public bool encrypt;
    }
}
