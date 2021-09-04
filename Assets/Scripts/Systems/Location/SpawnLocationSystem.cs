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
        private readonly EcsFilter<StartGameEvent, UpdateLevelValueEvent> _updateLevelFilter = null;
        
        public void Run()
        {
            foreach (var i in _updateLevelFilter)
            {
                SpawnLocation(_updateLevelFilter.Get2(i).CurrentLevel);
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
            
            //var destroyableObectsComponent
            
            var locationSpawnEvent = new LocationSpawnEvent() { };
            
            _world.NewEntity().Replace(locationComponent).Replace(partsComponent).Replace(pipeComponent).Replace(locationSpawnEvent);
        }
    }
}