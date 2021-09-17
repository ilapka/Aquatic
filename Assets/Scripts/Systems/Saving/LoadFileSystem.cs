using System.IO;
using Components;
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
        
        private readonly SavingSettings _savingSettings = null;
        private readonly GameProgressSavedData _gameProgressData = null;

        public void Init()
        {
            var partPath = Path.Combine(_savingSettings.encrypt ? _savingSettings.pathToEncryptFile : _savingSettings.pathToFile);
            var root = Application.platform == RuntimePlatform.Android ? Application.persistentDataPath : Application.dataPath;
            var path = Path.Combine(root, partPath);
            _world.NewEntity().Get<SavingPathComponent>().FilePath = path;
            
            if (!File.Exists(path))
            {
                _gameProgressData.levelValue = 0;
                _gameProgressData.playerMoney = 0;
                _world.NewEntity().Get<PlayerSavesLoadedEvent>();
                return;
            }
            
            var dataToLoad = File.ReadAllText(path);
            if (_savingSettings.encrypt)
            {
                dataToLoad = EncryptionManager.AESDecryption(dataToLoad);
            }
            JsonUtility.FromJsonOverwrite(dataToLoad, _gameProgressData);
            _world.NewEntity().Get<PlayerSavesLoadedEvent>();
        }
    }
}