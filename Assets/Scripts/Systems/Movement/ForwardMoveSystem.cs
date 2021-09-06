using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems.Movement
{
    public sealed class ForwardMoveSystem : IEcsRunSystem
    {
        private readonly EcsFilter<GameStateComponent> _gameStateFilter = null;
        private readonly EcsFilter<MovableComponent, ForwardMovableComponent> _forwardMoveFilter = null;

        public void Run()
        {
            foreach (var i in _gameStateFilter)
            {
                if(!_gameStateFilter.Get1(i).IsGamePlayProcess) return;;
                
                foreach (var j in _forwardMoveFilter)
                {
                    ref var movableTransform = ref  _forwardMoveFilter.Get1(j).Transform;
                    var forwardMoveComponent = _forwardMoveFilter.Get2(j);
                    var forwardMoveData = forwardMoveComponent.ForwardMoveData;
                
                    var offset = Vector3.Normalize(forwardMoveData.direction);
                
                    movableTransform.position = Vector3.MoveTowards(movableTransform.position,
                        movableTransform.position + offset, Time.deltaTime * forwardMoveData.speed);
                }
            }
        }
    }
}
