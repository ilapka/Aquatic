﻿using System.Collections.Generic;
using Components;
using Components.Events;
using Data;
using Leopotam.Ecs;
using Types;
using UnityComponents;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Systems.PipeRing
{
    public sealed class GenerationPipeRingsSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<LocationComponent, PipeComponent> _locationFilter  = null;
        private readonly EcsFilter<PlayerComponent> _playerFilter = null;
        private readonly EcsFilter<RubbishComponent> _rubbishFilter = null;

        public void Run()
        {
            foreach (var i in _playerFilter)
            {
                var playerTransform = _playerFilter.Get1(i).PlayerInformation.transform;
                
                foreach (var j in _locationFilter)
                {
                    var ringsGenerationData = _locationFilter.Get1(j).RingsGenerationData;
                    var ringsContainer = _locationFilter.Get1(j).LocationInformation.pipeRingsContainer;
                    ref var pipeComponent = ref _locationFilter.Get2(j);
            
                    if (playerTransform.position.x - ringsGenerationData.generationDistanceFromPlayer <= pipeComponent.LastRingEdge.x)
                    {
                        var ringSettings = GetRandomRing(ringsGenerationData.generationRingSettings);
                        var prefab = ringsGenerationData.ringsListData.pipeRingsList
                            .Find(ring => ring.ringType == ringSettings.pipeRingType).pipeRingInformation;

                        var count = Random.Range(ringSettings.minCountInRow, ringSettings.maxCountInRow);
                
                        for (int k = 0; k < count; k++)
                        {
                            var ringInformation = Object.Instantiate(prefab, pipeComponent.LastRingEdge, prefab.transform.rotation, ringsContainer);
                            pipeComponent.LastRingEdge = ringInformation.endPoint.position;

                            SubscribeRubbish(ringInformation);
                            SubscribeDestroyable(ringInformation, ringSettings.pipeRingType);
                        }
                    }
                }
            }
        }

        private void SubscribeRubbish(PipeRingInformation ringInformation)
        {
            foreach (var m in _rubbishFilter)
            {
                _rubbishFilter.Get1(m).RubbishList.Add(ringInformation.rubbishInstance);
            }
        }
        
        private void SubscribeDestroyable(PipeRingInformation ringInformation, PipeRingType pipeRingType)
        {
            if (ringInformation is DestructiblePipeRingInformation destructiblePipeRingInformation)
            {
                var addNewDestroyableEvent = new AddNewDestroyableObjectEvent()
                {
                    DestroyableObjects = destructiblePipeRingInformation.destroyableInstance,
                    PipeRingType = pipeRingType
                };
                _world.NewEntity().Replace(addNewDestroyableEvent);
            }
        } 

        private GenerationRingSetting GetRandomRing(List<GenerationRingSetting> ringSettings)
        {
            var maxRandomWeight = 0f;
            GenerationRingSetting randomRing = default;

            foreach (var ring in ringSettings)
            {
                if (Random.Range(0f, ring.weightOfChanceToSpawn) >= maxRandomWeight)
                {
                    maxRandomWeight = ring.weightOfChanceToSpawn;
                    randomRing = ring;
                }
            }
            
            return randomRing;
        }
    }
}
