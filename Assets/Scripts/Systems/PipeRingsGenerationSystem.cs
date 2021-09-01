using System;
using Components;
using Components.Events;
using Leopotam.Ecs;
using Data;
using UnityComponents;
using UnityEngine;
using Object = UnityEngine.Object;

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
        private PipeGenerationData _pipeGenerationData;
        private bool _initialized;

        public void Run()
        {
            if(!_initialized)
            { 
                foreach (var i in _locationSpawnEvent)
                {
                    _pipeGenerationData = _locationSpawnEvent.Get1(i).PipeGenerationData;
                    _pipeRingContainer = _locationSpawnEvent.Get1(i).LocationInformation.pipeRingsContainer;
                    _edgePoint = _pipeRingContainer.position;

                    //GenerateRings();
                    
                    _initialized = true;
                }
            }

            foreach (var i in _playerFilter)
            {
                var playerTransform = _playerFilter.Get1(i).PlayerInformation.transform;
                
                if (playerTransform.position.x <= _edgePoint.x - _pipeGenerationData.generateDistance)
                {
                    //GenerateRings();
                }
            }
        }

        private void GenerateRings(PipeRingInformation prefab, int count, Transform ringsContainer)
        {
            for (int i = 0; i < count; i++)
            {
                var ringInformation = Object.Instantiate(prefab, _edgePoint, Quaternion.identity, ringsContainer);

                _edgePoint = ringInformation.endPoint.position;
            }
        }
    }
}
