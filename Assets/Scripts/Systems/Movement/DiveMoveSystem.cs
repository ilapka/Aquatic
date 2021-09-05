using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems.Movement
{
    public sealed class DiveMoveSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InputComponent> _inputEventFilter = null;
        private readonly EcsFilter<MovableComponent, DiveMovableComponent> _diveMoveFilter = null;


        public void Run()
        {
            foreach (var i in _inputEventFilter)
            {
                var isTouch = _inputEventFilter.Get1(i).IsTouch;
                
                foreach (var j in _diveMoveFilter)
                {
                    ref var movableTransform = ref _diveMoveFilter.Get1(j).Transform;
                    var diveMovableComponent = _diveMoveFilter.Get2(j);
                    var diveMoveData = diveMovableComponent.DiveMoveData;

                    Vector3 destinationPoint;
                    var localPosition = movableTransform.localPosition;

                    if (isTouch)
                    {
                        if (movableTransform.localPosition.y - diveMovableComponent.StartLocalPosition.y <= diveMoveData.maxDepth)
                        {
                            return;
                        }
                        destinationPoint = new Vector3(localPosition.x, diveMovableComponent.StartLocalPosition.y + diveMoveData.maxDepth, localPosition.z);
                    }
                    else
                    {
                        if (movableTransform.localPosition.y >= diveMovableComponent.StartLocalPosition.y)
                        {
                            return;
                        }
                        destinationPoint = new Vector3(localPosition.x, diveMovableComponent.StartLocalPosition.y, localPosition.z);
                    }
                    movableTransform.localPosition = Vector3.Lerp(movableTransform.localPosition,
                        destinationPoint, Time.deltaTime * diveMoveData.divingSpeed);
                }
            }
        }
    }
}
