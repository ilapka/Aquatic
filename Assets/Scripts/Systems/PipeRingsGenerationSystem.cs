using System;
using Components;
using Components.Events;
using Leopotam.Ecs;
using Data;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Systems
{
    public sealed class PipeRingsGenerationSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly LevelListData _levelListData = null;

        private readonly EcsFilter<UpdateLevelValueEvent> _updateLevelFilter  = null;
        private readonly EcsFilter<PlayerComponent> _playerFilter  = null;
        
        public void Run()
        {
            
            
        }
    }
}
