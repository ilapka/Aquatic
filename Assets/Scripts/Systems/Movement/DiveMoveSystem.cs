using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems.Movement
{
    public class DiveMoveSystem : IEcsRunSystem
    {
        private EcsFilter<InputComponent> _inputEventFilter = null;
        private EcsFilter<MovableComponent, DiveMovableComponent> _diveMoveFilter = null;


        public void Run()
        {
            var isTouch = false;
            
            foreach (var i in _inputEventFilter)
            {
                isTouch = _inputEventFilter.Get1(i).IsTouch;
            }
            
            foreach (var i in _diveMoveFilter)
            {
                ref var movableComponent = ref _diveMoveFilter.Get1(i);
                var diveMovableComponent = _diveMoveFilter.Get2(i);

                var movableTransform = movableComponent.Transform;
                var diveMoveData = diveMovableComponent.DiveMoveData;

                Vector3 offset;
                    
                if (isTouch)
                {
                    offset = Vector3.Normalize(diveMoveData.diveDirection) * diveMoveData.divingSpeed;
                }
                else
                {
                    if(movableTransform.position.y >= diveMovableComponent.StartYPosition)
                        return;
                    
                    offset = Vector3.Normalize(-diveMoveData.diveDirection) * diveMoveData.surfacingSpeed;
                }
                
                movableComponent.MoveOffset += offset;
            }
        }
    }
}
