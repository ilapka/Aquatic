using System;
using System.Collections.Generic;
using Components;
using Components.Events;
using Leopotam.Ecs;
using Data;
using Types;
using UnityComponents;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Systems
{
    public sealed class PipeRingsGenerationSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<LocationComponent, PipeComponent, PlayerComponent> _locationFilter  = null;

        public void Run()
        {
            foreach (var i in _locationFilter)
            {
                var ringsGenerationData = _locationFilter.Get1(i).RingsGenerationData;
                var ringsContainer = _locationFilter.Get1(i).LocationInformation.pipeRingsContainer;
                ref var pipeComponent = ref _locationFilter.Get2(i);
                var playerTransform = _locationFilter.Get3(i).PlayerInformation.transform;
                
                if (playerTransform.position.x - ringsGenerationData.generationDistanceFromPlayer <= pipeComponent.LastRingEdge.x)
                {
                    var ringSettings = GetRandomRing(ringsGenerationData.generationRingSettings);
                    var prefab = ringsGenerationData.ringsListData.pipeRingsList
                        .Find(ring => ring.ringType == ringSettings.pipeRingType).pipeRingInformation;

                    var count = Random.Range(ringSettings.minCountInRow, ringSettings.maxCountInRow);
                    
                    for (int j = 0; j < count; j++)
                    {
                        var ringInformation = Object.Instantiate(prefab, pipeComponent.LastRingEdge, prefab.transform.rotation, ringsContainer);

                        pipeComponent.LastRingEdge = ringInformation.endPoint.position;
                    }
                }
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
