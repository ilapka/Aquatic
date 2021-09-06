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
            var levelProgressBarInformation = Object.Instantiate(_uiData.levelProgressBarPrefab, canvasInformation.uiContainer);
            var playerMoneyUiInformation = Object.Instantiate(_uiData.playerMoneyTextProPrefab, canvasInformation.uiContainer);
            var startPanelInformation = Object.Instantiate(_uiData.startPanelPrefab, canvasInformation.uiContainer);
            
            var uiEntity = _world.NewEntity();
            uiEntity.Get<CanvasComponent>().CanvasInformation = canvasInformation;
            uiEntity.Get<LevelProgressBarUIComponent>().LevelProgressBarInformation = levelProgressBarInformation;
            uiEntity.Get<MoneyUIComponent>().UIMoneyProInformation = playerMoneyUiInformation;
            uiEntity.Get<StartPanelComponent>().StartPanelInformation = startPanelInformation;
        }
    }
}
