using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems.Movement
{
    public class ForwardMoveSystem : IEcsRunSystem
    {
        private EcsFilter<MovableComponent, ForwardMovableComponent> _forwardMoveFilter = null;

        public void Run()
        {
            foreach (var i in _forwardMoveFilter)
            {
                ref var movableComponent = ref _forwardMoveFilter.Get1(i);
                var forwardMoveComponent = _forwardMoveFilter.Get2(i);
                var forwardMoveData = forwardMoveComponent.ForwardMoveData;
                
                var offset = Vector3.Normalize(forwardMoveData.direction) * forwardMoveData.speed;
                
                movableComponent.MoveOffset += offset;
            }
        }
    }
}
