using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Components;

namespace Systems
{
    public class InputSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world = null;
        private EcsFilter<InputComponent> _inputFilter = null;
        
        public void Init()
        {
            _world.NewEntity().Get<InputComponent>();
        }

        public void Run()
        {
            foreach (var i in _inputFilter)
            {
                ref var inputComponent = ref _inputFilter.Get1(i);
                inputComponent.IsTouch = Input.GetMouseButton(0);
            }
        }
    }
}
