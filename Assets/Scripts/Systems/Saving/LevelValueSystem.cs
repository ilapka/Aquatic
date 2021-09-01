using System.IO;
using System.Security.Cryptography;
using Components.Events;
using Data;
using Leopotam.Ecs;
using Managers;
using UnityEngine;

namespace Systems.Saving
{
    public sealed class LevelValueSystem : IEcsInitSystem ,IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly GameProgressData _gameProgressData;

        private readonly EcsFilter<LevelUpEvent> _levelUpFilter;

        public void Init()
        {
            UpdateLevelValue();
        }
        
        public void Run()
        {
            foreach (var i in _levelUpFilter)
            {
                _gameProgressData.levelValue++;
                _world.NewEntity().Get<SaveDataEvent>();
                UpdateLevelValue();
            }
        }

        private void UpdateLevelValue()
        {
            var uodateEvent = new UpdateLevelValueEvent(){ CurrentLevel = _gameProgressData.levelValue};
            _world.NewEntity().Replace(uodateEvent);
        }
    }
}