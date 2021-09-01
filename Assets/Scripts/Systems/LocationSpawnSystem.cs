using System;
using Components.Events;
using Leopotam.Ecs;
using Data;
using Object = UnityEngine.Object;

namespace Systems
{
    public sealed class LocationSpawnSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly LevelListData _levelListData = null;

        private readonly EcsFilter<UpdateLevelValueEvent> _updateLevelFilter;
        private bool _isSpawned;

        public void Run()
        {
            if(_isSpawned) return;

            foreach (var i in _updateLevelFilter)
            {
                SpawnLocation(_updateLevelFilter.Get1(i).CurrentLevel);
                _isSpawned = true;
            }
        }

        private void SpawnLocation(int levelValue)
        {
            var locationInformation = Object.Instantiate(_levelListData.levelList[levelValue].locationInformation);
        }
    }
}
