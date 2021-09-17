using Components;
using Components.Events;
using Data;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems.Saving
{
    public sealed class LevelValueSystem : IEcsInitSystem ,IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly GameProgressSavedData _gameProgressData = null;

        private readonly EcsFilter<LevelUpEvent> _levelUpFilter = null;
        private readonly EcsFilter<PlayerSavesLoadedEvent> _savesLoadedFilter = null;

        public static int CurrentLevel { get; private set; }

        public void Init()
        {
            foreach (var i in _savesLoadedFilter)
            {
                CurrentLevel = _gameProgressData.levelValue;
                _world.NewEntity().Get<CreateLevelEvent>().LevelValue = _gameProgressData.levelValue;
                Debug.Log("Create Level Event");
            }
        }
        
        public void Run()
        {
            foreach (var i in _levelUpFilter)
            {
                _gameProgressData.levelValue++;
                CurrentLevel = _gameProgressData.levelValue;
                _world.NewEntity().Get<SaveDataEvent>();
                Debug.Log($"New level value - {_gameProgressData.levelValue}");
            }
        }
    }
}