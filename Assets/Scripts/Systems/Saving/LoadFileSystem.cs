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
        private readonly GameProgressData _gameProgressData = null;

        public void Init()
        {
            var pathInside = _savingSettings.encrypt ? _savingSettings.pathToEncryptFile : _savingSettings.pathToFile;
            var path = Application.dataPath + pathInside;

            var savingEntity = _world.NewEntity();
            savingEntity.Get<SavingComponent>().FilePath = path;

            if (!File.Exists(path))
            {
                savingEntity.Get<SaveDataEvent>();
                return;
            }
            
            var dataToLoad = File.ReadAllText(path);
            if (_savingSettings.encrypt)
            {
                dataToLoad = EncryptionManager.AESDecryption(dataToLoad);
            }
            JsonUtility.FromJsonOverwrite(dataToLoad, _gameProgressData);
        }
    }
}