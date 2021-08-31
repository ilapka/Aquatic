using System.IO;
using Components.Events;
using Data;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems.Saving
{
    public sealed class LoadFileSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        
        private readonly SavingSettings _savingSettings;
        private GameProgressData _gameProgressData;

        public void Init()
        {
            var path = Application.dataPath + "/" + _savingSettings.pathToSaving + "/" + _savingSettings.dataFileName + ".json";

            if (!File.Exists(path))
            {
                _world.NewEntity().Get<SaveDataEvent>();
                return;
            }

            var dataToLoad = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(dataToLoad, _gameProgressData);
        }
    }
}