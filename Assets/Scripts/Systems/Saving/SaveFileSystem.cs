using System;
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
        private readonly EcsFilter<SavingPathComponent> _savingPathComponent = null;
        private readonly EcsFilter<SaveDataEvent> _saveDataFilter = null;

        private readonly SavingSettings _savingSettings = null;
        private readonly GameProgressSavedData _gameProgressData = null;

        public void Run()
        {
            foreach (var i in _saveDataFilter)
            {
                foreach (var j in _savingPathComponent)
                {
                    var path = _savingPathComponent.Get1(j).FilePath;
                    var directory = Path.GetDirectoryName(path);
                    
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    if (!File.Exists(path))
                    {
                        var fs = File.Create(path);
                        fs.Close();
                    }

                    var dataToSave = JsonUtility.ToJson(_gameProgressData);
                    if (_savingSettings.encrypt)
                    {
                        dataToSave = EncryptionManager.AESEncryption(dataToSave);
                    }
                    File.WriteAllText(path, dataToSave);
                    Debug.Log($"Data saved");
                }
            }
        }
    }
}