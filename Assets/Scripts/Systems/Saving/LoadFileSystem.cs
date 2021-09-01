using System.IO;
using Components.Events;
using Data;
using Leopotam.Ecs;
using Managers;
using UnityEngine;

namespace Systems.Saving
{
    public sealed class LoadFileSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        
        private readonly SavingSettings _savingSettings;
        private readonly GameProgressData _gameProgressData;

        public void Init()
        {
            var postfix = _savingSettings.encryptFiles ? _savingSettings.encryptPostfix : "";
            var path = $"{Application.dataPath}/{_savingSettings.pathToSaving}/{_savingSettings.dataFileName}{postfix}.json";

            if (!File.Exists(path))
            {
                _world.NewEntity().Get<SaveDataEvent>();
                return;
            }
            
            var dataToLoad = File.ReadAllText(path);
            if (_savingSettings.encryptFiles)
            {
                dataToLoad = EncryptionManager.AESDecryption(dataToLoad);
            }
            JsonUtility.FromJsonOverwrite(dataToLoad, _gameProgressData);
        }
    }
}