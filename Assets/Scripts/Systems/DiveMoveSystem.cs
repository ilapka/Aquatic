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
            foreach (var i in _diveMoveFilter)
            {
                var movableComponent = _diveMoveFilter.Get1(i);
                var diveMovableComponent = _diveMoveFilter.Get2(i);
                
            }
        }
    }
}
