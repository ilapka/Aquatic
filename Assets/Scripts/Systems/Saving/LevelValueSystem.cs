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
        private readonly EcsFilter<SavingComponent> _savingFilter = null;

        public static int CurrentLevel { get; private set; }

        public void Init()
        {
            CurrentLevel = _gameProgressData.levelValue;
            _world.NewEntity().Get<UpdateLevelValueEvent>().CurrentLevel = _gameProgressData.levelValue;
        }
        
        public void Run()
        {
            foreach (var i in _levelUpFilter)
            {
                _gameProgressData.levelValue++;
                CurrentLevel = _gameProgressData.levelValue;
                foreach (var j in _savingFilter)
                {
                    _savingFilter.GetEntity(j).Get<SaveDataEvent>();
                }
            }
        }
    }
}