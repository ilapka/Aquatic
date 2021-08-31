using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Components;
using Data;
using Information;

namespace Systems
{
    public class LocationSpawnSystem : IEcsInitSystem
    {
        private EcsWorld _world = null;
        private LevelListData _levelListData = null;

        public void Init()
        {
            
        }
    }
}
