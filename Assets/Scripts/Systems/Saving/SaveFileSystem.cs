using System.IO;
using Components.Events;
using Data;
using Leopotam.Ecs;
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
                var dataToSave = JsonUtility.ToJson(_gameProgressData);
                var path = Application.dataPath + "/" + _savingSettings.pathToSaving + "/" + _savingSettings.dataFileName + ".json";
                File.WriteAllText(path, dataToSave);

                Debug.Log("File saved");
            }
        }
    }
}