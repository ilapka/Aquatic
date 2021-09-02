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
        private readonly LevelListData _levelListData = null;

        private readonly EcsFilter<LocationSpawnEvent> _locationSpawnEvent = null;
        private readonly EcsFilter<PlayerComponent> _playerFilter  = null;

        private Transform _pipeRingContainer;
        private Vector3 _edgePoint;
        private RingsGenerationData _ringsGenerationData;
        private bool _initialized;

        public void Run()
        {
            if(!_initialized)
            { 
                foreach (var i in _locationSpawnEvent)
                {
                    _ringsGenerationData = _locationSpawnEvent.Get1(i).RingsGenerationData;
                    _pipeRingContainer = _locationSpawnEvent.Get1(i).LocationInformation.pipeRingsContainer;
                    _edgePoint = _pipeRingContainer.position;

                    //определяем какой тип кольца и какая длинна далее:
                    //GenerateRings();
                    
                    _initialized = true;
                }
            }

            foreach (var i in _playerFilter)
            {
                var playerTransform = _playerFilter.Get1(i).PlayerInformation.transform;
                
                if (playerTransform.position.x <= _edgePoint.x - _ringsGenerationData.generationDistanceFromPlayer)
                {
                    //определяем какой тип кольца и какая длинна далее:
                    //GenerateRings();
                }
            }
        }

        private void GenerateRandomRings()
        {
            //var type = GetRandomRingType();
        }

        private PipeRingType GetRandomRingType(List<GenerationRingSetting> ringSettings)
        {
            var maxRandomWeight = 0f;
            var randomType = PipeRingType.Default;

            foreach (var ring in ringSettings)
            {
                if (Random.Range(0f, ring.weightOfChanceToSpawn) >= maxRandomWeight)
                {
                    maxRandomWeight = ring.weightOfChanceToSpawn;
                    randomType = ring.pipeRingType;
                }
            }
            
            return randomType;
        }

        private void SpawnRings(PipeRingType ringType, int count, Transform ringsContainer, RingsListData ringsListData)
        {
            var prefab = ringsListData.pipeRingsList.Find(ring => ring.ringType == ringType).pipeRingInformation;
            
            for (int i = 0; i < count; i++)
            {
                var ringInformation = Object.Instantiate(prefab, _edgePoint, Quaternion.identity, ringsContainer);

                _edgePoint = ringInformation.endPoint.position;
            }
        }
    }
}
