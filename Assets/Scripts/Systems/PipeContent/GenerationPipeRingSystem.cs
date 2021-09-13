using System.Collections.Generic;
using Components;
using Components.Events;
using Data;
using Leopotam.Ecs;
using UnityComponents;
using UnityComponents.Information;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Systems.PipeContent
{
    public sealed class GenerationPipeRingSystem : IEcsRunSystem
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
                        var generationSettings = GetRandomContent(ringsGenerationData.generationRingSettings);
                        var ringSettings = ringsGenerationData.ringsList.pipeRingsList.Find(ring => ring.contentType == generationSettings.pipeContentType);
                        var prefab = ringSettings.pipeRingInformation;

                        var count = Random.Range(generationSettings.minCountInRow, generationSettings.maxCountInRow);
                
                        for (int k = 0; k < count; k++)
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

        private GenerationRingSetting GetRandomContent(List<GenerationRingSetting> generationRingsSettings)
        {
            var maxRandomWeight = 0f;
            GenerationRingSetting randomGenerationRingSetting = default;

            foreach (var generationRingSetting in generationRingsSettings)
            {
                if (Random.Range(0f, generationRingSetting.weightOfChanceToSpawn) >= maxRandomWeight)
                {
                    maxRandomWeight = generationRingSetting.weightOfChanceToSpawn;
                    randomGenerationRingSetting = generationRingSetting;
                }
            }
            
            return randomGenerationRingSetting;
        }
    }
}
