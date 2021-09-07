using System;
using Components;
using Components.Events;
using Data;
using Leopotam.Ecs;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Systems.UI
{
    public class CanvasSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        private readonly UIData _uiData = null;
        
        public void Init()
        {
            var canvasInformation = Object.Instantiate(_uiData.canvasPrefab);
            
            var uiEntity = _world.NewEntity();
            uiEntity.Get<CanvasComponent>().CanvasInformation = canvasInformation;
        }
    }
}
