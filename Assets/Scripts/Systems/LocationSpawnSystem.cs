using System;
using Components;
using Components.Events;
using Leopotam.Ecs;
using Data;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Systems
{
    public sealed class LocationSpawnSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly LevelListData _levelListData = null;

        private readonly EcsFilter<UpdateLevelValueEvent> _updateLevelFilter = null;
        private readonly EcsFilter<PlayerComponent> _playerFilter = null;
        
        private Transform _firstLocationPart;
        private Transform _secondLocationPart;
        private float _extendablePartDistance;
        private bool _isSpawned;

        public void Run()
        {
            if (!_isSpawned)
            {
                foreach (var i in _updateLevelFilter)
                {
                    SpawnLocation(_updateLevelFilter.Get1(i).CurrentLevel);
                    _isSpawned = true;
                }
            }

            foreach (var i in _playerFilter)
            {
                var player = _playerFilter.Get1(i).PlayerInformation;

                if (player.transform.position.x <= _secondLocationPart.transform.position.x)
                {
                    ExtendLocation();
                }
            }
        }

        private void SpawnLocation(int levelValue)
        {
            var levelStruct = _levelListData.levelList[levelValue];
            var locationInformation = Object.Instantiate(levelStruct.levelInformation);

            _firstLocationPart = locationInformation.firstEnvironmentPart;
            _secondLocationPart = locationInformation.secondEnvironmentPart;
            _extendablePartDistance = Mathf.Abs(_firstLocationPart.position.x - _secondLocationPart.position.x);

            var locationSpawnEvent = new LocationSpawnEvent(){ LocationInformation = locationInformation};
            _world.NewEntity().Replace(locationSpawnEvent);
        }

        private void ExtendLocation()
        {
            var temp = _secondLocationPart;
            _firstLocationPart.position = new Vector3(_secondLocationPart.position.x - _extendablePartDistance,
                _secondLocationPart.position.y,_secondLocationPart.position.z);
            _secondLocationPart = _firstLocationPart;
            _firstLocationPart = temp;
        }
    }
}
