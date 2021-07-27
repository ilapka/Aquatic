using Leopotam.Ecs;
using Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Systems
{
    public class MoveSystem : IEcsRunSystem
    {
        //Filter find all entities, that contain InputEventComponent and MovableComponent
        private EcsFilter<InputEventComponent, MovableComponent> _playerMoveFilter = null;

        public void Run()
        {
            foreach (var i in _playerMoveFilter)
            {
                var inputComponent = _playerMoveFilter.Get1(i);
                var movableComponent = _playerMoveFilter.Get2(i);
                
                if (!inputComponent.isTouching) continue;
                movableComponent.rigidbody.AddForce(movableComponent.transform.forward * (Time.fixedTime * movableComponent.acceleration), ForceMode.Acceleration);

                //movableComponent.currentSpeed = Mathf.MoveTowards(movableComponent.currentSpeed, inputComponent.isTouching ? movableComponent.maxMoveSpeed : 0f, movableComponent.acceleration * Time.deltaTime);
                //movableComponent.transform.Translate(Vector3.forward * (movableComponent.currentSpeed * Time.deltaTime));
            }
        }

    }
}
