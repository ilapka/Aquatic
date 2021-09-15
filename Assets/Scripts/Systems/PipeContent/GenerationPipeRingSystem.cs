using System.Collections.Generic;
using Components;
using Components.Events;
using Data;
using Leopotam.Ecs;
using Types;
using UnityComponents;
using UnityComponents.Information;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Systems.PipeContent
{
    public sealed class GenerationPipeRingSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<LocationComponent, LocationSpawnEvent> _spawnLocationFilter = null;
        private readonly EcsFilter<LocationComponent, PipeComponent> _locationFilter  = null;
        private readonly EcsFilter<PlayerComponent> _playerFilter = null;
        private readonly EcsFilter<RubbishComponent> _rubbishFilter = null;
        private readonly EcsFilter<PipeRingWeightsComponent> _pipeRingWeightsFilter = null;

        public void Run()
        {
            foreach (var i in _spawnLocationFilter)
            {
                var ringsGenerationData = _locationFilter.Get1(i).RingsGenerationData;
                var ringsInLineWeightDictionary = new Dictionary<PipeRingType, float>();
                var totalWeight = 0f;
                foreach (var generationRingSetting in ringsGenerationData.generationRingSettings)
                {
                    totalWeight += generationRingSetting.weightOfChanceToSpawn;
                    ringsInLineWeightDictionary.Add(generationRingSetting.pipeRingType, totalWeight);
                }

                var pipeRingWeightsComponent = new PipeRingWeightsComponent()
                {
                    RingsInLineWeightDictionary = ringsInLineWeightDictionary,
                    TotalWeight = totalWeight,
                };
                _world.NewEntity().Replace(pipeRingWeightsComponent);
            }
            
            foreach (var i in _playerFilter)
            {
                var playerTransform = _playerFilter.Get1(i).PlayerInformation.transform;
                
                foreach (var j in _locationFilter)
                {
                    var ringsGenerationData = _locationFilter.Get1(j).RingsGenerationData;
                    var ringsContainer = _locationFilter.Get1(j).LocationInformation.pipeRingsContainer;
                    ref var pipeComponent = ref _locationFilter.Get2(j);
            
                    while (playerTransform.position.x - ringsGenerationData.generationDistanceFromPlayer <= pipeComponent.LastRingEdge.x)
                    {
                        var ringType = GetRandomRingType();
                        var generationSettings = ringsGenerationData.generationRingSettings.Find(ringSetting => ringSetting.pipeRingType == ringType);
                        var ringSettings = ringsGenerationData.ringsList.pipeRingsList.Find(ring => ring.ringType == ringType);
                        var prefab = ringSettings.pipeRingInformation;

                        var count = Random.Range(generationSettings.minCountInRow, generationSettings.maxCountInRow);
                
                        for (var k = 0; k < count; k++)
                        {
                            var ringInformation = Object.Instantiate(prefab, pipeComponent.LastRingEdge, prefab.transform.rotation, ringsContainer);
                            pipeComponent.LastRingEdge = ringInformation.endPoint.position;

                            SubscribeRubbish(ringInformation);
                            SubscribeDestroyable(ringInformation, ringSettings);
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
        
        private void SubscribeDestroyable(PipeRingInformation ringInformation, PipeRingStruct pipeRingSettings)
        {
            if (ringInformation is DestructiblePipeRingInformation destructiblePipeRingInformation)
            {
                var addNewDestroyableEvent = new AddNewDestroyableObjectEvent()
                {
                    DestroyableObjects = destructiblePipeRingInformation.destroyableInstance,
                    PipeRingSettings = pipeRingSettings
                };
                _world.NewEntity().Replace(addNewDestroyableEvent);
            }
        }

        private PipeRingType GetRandomRingType()
        {
            PipeRingType ringType = default;
            foreach (var i in _pipeRingWeightsFilter)
            {
                var pipeRingWeightsComponent = _pipeRingWeightsFilter.Get1(i);
                var randomWeight = Random.Range(0f, pipeRingWeightsComponent.TotalWeight);

                foreach (var ringWeightPair in pipeRingWeightsComponent.RingsInLineWeightDictionary)
                {
                    if (randomWeight <= ringWeightPair.Value)
                    {
                        ringType = ringWeightPair.Key;
                        break;
                    }
                }
            }
            //Debug.Log(ringType);
            return ringType;
        }
    }
}
