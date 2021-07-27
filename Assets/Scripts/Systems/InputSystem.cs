using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Components;

namespace Systems
{
    public class InputSystem : IEcsRunSystem
    {
        //Filter find all entities, that contain InputEventComponent
        private EcsFilter<InputEventComponent> _inputEventFilter = null;

        public void Run()
        {
            var isTouching = Input.GetMouseButton(0);

            foreach (var i in _inputEventFilter)
            {
                #region Example GetEntity
                //entity that contains WeaponComponent.
                ref var entity = ref _inputEventFilter.GetEntity(i);
                #endregion

                //Get1 will return link to attached "InputEventComponent".
                ref var inputEvent = ref _inputEventFilter.Get1(i);
                inputEvent.isTouching = isTouching;
            }
        }
    }
}
