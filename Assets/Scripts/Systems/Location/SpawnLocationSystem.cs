﻿using Components;
using Components.Events;
using Data;
using Leopotam.Ecs;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Systems.Location
{
    public sealed class SpawnLocationSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly LevelListData _levelListData = null;

        private readonly EcsFilter<LocationComponent> _locationFilter = null;
        private readonly EcsFilter<UpdateLevelValueEvent> _updateLevelValueEvent = null;
        
        public void Run()
        {
            if(!_locationFilter.IsEmpty()) return;
            
            foreach (var i in _updateLevelValueEvent)
            {
                SpawnLocation(_updateLevelValueEvent.Get1(i).CurrentLevel);
            }
        }

        private void SpawnLocation(int levelValue)
        {
            var locationIndex = levelValue % _levelListData.levelList.Count;    
            var levelStruct = _levelListData.levelList[locationIndex];  
            var locationInformation = Object.Instantiate(levelStruct.levelInformation);  

            var locationComponent = new LocationComponent()
            {
                LocationInformation = locationInformation,
                RingsGenerationData = levelStruct.ringsGenerationSettings
            };
            
            var partsComponent = new LocationPartsComponent()
            {
                FirstLocationPart = locationInformation.firstEnvironmentPart,
                SecondLocationPart = locationInformation.secondEnvironmentPart,
                DistanceBetween = Mathf.Abs(locationInformation.firstEnvironmentPart.position.x - locationInformation.secondEnvironmentPart.position.x)
            };
            
            var pipeComponent = new PipeComponent()
            {
                LastRingEdge = locationInformation.pipeRingsContainer.position,
            };

            var locationSpawnEvent = new LocationSpawnEvent() { };
            
            _world.NewEntity().Replace(locationComponent).Replace(partsComponent).Replace(pipeComponent).Replace(locationSpawnEvent);
            
            _world.NewEntity().Get<LevelComponent>().LevelData = levelStruct.levelData;
        }
    }
}
