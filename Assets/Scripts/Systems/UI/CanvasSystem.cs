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
            var levelProgressBarInformation = Object.Instantiate(_uiData.levelProgressBarPrefab, canvasInformation.gameUIContainer);
            var playerMoneyUiInformation = Object.Instantiate(_uiData.playerMoneyTextProPrefab, canvasInformation.gameUIContainer);
            var uiEntity = _world.NewEntity();
            uiEntity.Get<CanvasUIComponent>().CanvasInformation = canvasInformation;
            uiEntity.Get<LevelProgressBarUIComponent>().LevelProgressBarInformation = levelProgressBarInformation;
            uiEntity.Get<MoneyUIComponent>().UIMoneyProInformation = playerMoneyUiInformation;
        }
    }
}
