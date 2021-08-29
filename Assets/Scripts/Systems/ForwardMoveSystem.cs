using Leopotam.Ecs;
using Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Systems
{
    public class ForwardMoveSystem : IEcsRunSystem
    {
        private EcsFilter<MovableComponent, ForwardMovableComponent> _forwardMoveFilter = null;

        public void Run()
        {
            foreach (var i in _forwardMoveFilter)
            {
                var movableComponent = _forwardMoveFilter.Get1(i);
                var forwardMoveComponent = _forwardMoveFilter.Get2(i);

                var movableRigidbody = movableComponent.Rigidbody;
                var forwardMoveData = forwardMoveComponent.ForwardMoveData;

                var offset = Vector3.Normalize(forwardMoveData.direction) * (forwardMoveData.speed * Time.fixedTime);
                movableRigidbody.MovePosition(movableRigidbody.transform.position + offset);
            }
        }
    }
}
