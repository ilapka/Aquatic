using Leopotam.Ecs;
using Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Systems
{
    public class ForwardMoveSystem : IEcsRunSystem
    {
        private EcsFilter<ForwardMovableComponent> _playerMoveFilter = null;

        public void Run()
        {
            foreach (var i in _playerMoveFilter)
            {
                var movableComponent = _playerMoveFilter.Get1(i);

               
            }
        }
    }
}
