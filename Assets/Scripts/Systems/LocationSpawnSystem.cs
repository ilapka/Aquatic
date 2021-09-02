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
        private readonly EcsFilter<LocationPartsComponent> _locationPartsFilter = null;
        
        private bool _isSpawned;

        public void Run()
        {
            if (!_isSpawned) //убрать флажок но добавить к фильтру компонент "Start game" или типа того
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
/*
                if (player.transform.position.x <= _secondLocationPart.transform.position.x)
                {
                    ExtendLocation();
                }*/
            }
        }

        private void SpawnLocation(int levelValue)
        {
            foreach (var i in _locationPartsFilter)
            {
                var parts = _locationPartsFilter.Get1(i);

                var locationNunber = levelValue % _levelListData.levelList.Count;
                Debug.Log($"Location number - {locationNunber}");
                var levelStruct = _levelListData.levelList[locationNunber];
                var locationInformation = Object.Instantiate(levelStruct.levelInformation);

                parts.FirstLocationPart = locationInformation.firstEnvironmentPart;
                parts.SecondLocationPart = locationInformation.secondEnvironmentPart;
                parts.DistanceBetween = Mathf.Abs(parts.FirstLocationPart.position.x - parts.SecondLocationPart.position.x);

                var locationSpawnEvent = new LocationSpawnEvent()
                {
                    LocationInformation = locationInformation,
                    RingsGenerationData = levelStruct.ringsGenerationSettings
                };
                _world.NewEntity().Replace(locationSpawnEvent);
            }
        }

        private void ExtendLocation()
        {
            foreach (var i in _locationPartsFilter)
            {
                var parts = _locationPartsFilter.Get1(i);

                var temp = parts.SecondLocationPart;
                var position = parts.SecondLocationPart.position;
                parts.FirstLocationPart.position = new Vector3(position.x - parts.DistanceBetween, position.y, position.z);
                parts.SecondLocationPart = parts.FirstLocationPart;
                parts.FirstLocationPart = temp;
            }
        }
    }
}
