using System.IO;
using System.Security.Cryptography;
using Components;
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
        private readonly EcsFilter<SavingComponent, SaveDataEvent> _dataToSaveFilter = null;
        
        private readonly SavingSettings _savingSettings = null;
        private readonly GameProgressSavedData _gameProgressData = null;

        public void Run()
        {
            foreach (var i in _dataToSaveFilter)
            {
                var path = _dataToSaveFilter.Get1(i).FilePath;
                
                var dataToSave = JsonUtility.ToJson(_gameProgressData);
                if (_savingSettings.encrypt)
                {
                    dataToSave = EncryptionManager.AESEncryption(dataToSave);
                }
                File.WriteAllText(path, dataToSave);
            }
        }
    }
}