using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems.Movement
{
    public sealed class MoveSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MovableComponent> _moveFilter = null;

        public void Run()
        {
            foreach (var i in _moveFilter)
            {
                ref var movableComponent = ref _moveFilter.Get1(i);
                var movableTransform = movableComponent.Transform;
                ref var moveOffset = ref movableComponent.MoveOffset;
                
                if (movableComponent.LocalSpaceMoving)
                {
                    movableTransform.localPosition = Vector3.MoveTowards(movableTransform.localPosition,
                        movableTransform.localPosition + moveOffset, Time.fixedTime);
                }
                else
                {
                    movableTransform.position = Vector3.MoveTowards(movableTransform.position,
                        movableTransform.position + moveOffset, Time.fixedTime);
                }

                moveOffset = Vector3.zero;
            }
        }
    }
}
