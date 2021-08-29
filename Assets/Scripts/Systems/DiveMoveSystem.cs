using Leopotam.Ecs;
using Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Systems
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

                var movableRigidbody = movableComponent.Rigidbody;
                var diveMoveData = diveMovableComponent.DiveMoveData;

                Vector3 offset;
                    
                if (isTouch)
                {
                    offset = Vector3.Normalize(diveMoveData.diveDirection) *
                             (diveMoveData.divingSpeed * Time.fixedTime);
                }
                else
                {
                    if(movableRigidbody.position.y >= diveMovableComponent.StartYPosition)
                        return;
                    
                    offset = Vector3.Normalize(-diveMoveData.diveDirection) * (diveMoveData.surfacingSpeed * Time.fixedTime);
                }
                
                movableComponent.MoveOffset += offset;
            }
        }
    }
}
