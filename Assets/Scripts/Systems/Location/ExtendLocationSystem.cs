using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems.Location
{
    public sealed class ExtendLocationSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;

        private readonly EcsFilter<PlayerComponent> _playerFilter = null;
        private readonly EcsFilter<LocationPartsComponent> _locationPartsFilter = null;
        
        public void Run()
        {
            foreach (var i in _playerFilter)
            {
                var player = _playerFilter.Get1(i).PlayerInformation.transform;

                foreach (var j in _locationPartsFilter)
                {
                    ref var parts = ref _locationPartsFilter.Get1(j);

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
}
