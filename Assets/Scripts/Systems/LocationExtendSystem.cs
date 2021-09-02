using System;
using Components;
using Components.Events;
using Leopotam.Ecs;
using Data;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Systems
{
    public sealed class LocationExtendSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;

        private readonly EcsFilter<LocationPartsComponent, PlayerComponent> _locationPartsFilter = null;
        
        public void Run()
        {
            foreach (var i in _locationPartsFilter)
            {
                var parts = _locationPartsFilter.Get1(i);
                var player = _locationPartsFilter.Get2(i).PlayerInformation.transform;

                if (player.position.x <= parts.SecondLocationPart.transform.position.x)
                {
                    var temp = parts.SecondLocationPart;
                    var position = parts.SecondLocationPart.position;
                    parts.FirstLocationPart.position = new Vector3(position.x - parts.DistanceBetween, position.y, position.z);
                    parts.SecondLocationPart = parts.FirstLocationPart;
                    parts.FirstLocationPart = temp;
                }
            }
        }
    }
}
