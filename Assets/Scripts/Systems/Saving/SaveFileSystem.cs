using System.IO;
using System.Security.Cryptography;
using Components.Events;
using Data;
using Leopotam.Ecs;
using Managers;
using UnityEngine;

namespace Systems.Saving
{
    public sealed class SaveFileSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<SaveDataEvent> _dataToSaveFilter;
        
        private readonly SavingSettings _savingSettings;
        private readonly GameProgressData _gameProgressData;

        public void Run()
        {
            foreach (var i in _dataToSaveFilter)
            {
                var postfix = _savingSettings.encryptFiles ? _savingSettings.encryptPostfix : "";
                var path = $"{Application.dataPath}/{_savingSettings.pathToSaving}/{_savingSettings.dataFileName}{postfix}.json";
                var dataToSave = JsonUtility.ToJson(_gameProgressData);
                if (_savingSettings.encryptFiles)
                {
                    dataToSave = EncryptionManager.AESEncryption(dataToSave);
                }
                File.WriteAllText(path, dataToSave);
            }
        }
    }
}