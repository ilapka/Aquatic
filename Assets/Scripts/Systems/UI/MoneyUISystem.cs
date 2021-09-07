using Components;
using Components.Events;
using Data;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems.UI
{
    public class MoneyUISystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly UIData _uiData = null;

        private readonly EcsFilter<CanvasComponent> _canvasFilter = null;
        private readonly EcsFilter<MoneyUIComponent> _uiFilter = null;
        private readonly EcsFilter<UpdateMoneyValueEvent> _updateMoneyEventFilter = null;

        public void Init()
        {
            foreach (var i in _canvasFilter)
            {
                ref var canvasComponent = ref _canvasFilter.Get1(i);
                var playerMoneyUiInformation = Object.Instantiate(_uiData.playerMoneyTextProPrefab, canvasComponent.CanvasInformation.uiContainer);
                _canvasFilter.GetEntity(i).Get<MoneyUIComponent>().UIMoneyProInformation = playerMoneyUiInformation;
            }
        }
        
        public void Run()
        {
            foreach (var i in _uiFilter)
            {
                ref var moneyUI = ref _uiFilter.Get1(i);

                if (moneyUI.UIMoneyProInformation == null) return;
             
                foreach (var j in _updateMoneyEventFilter)
                {
                    var newValue = _updateMoneyEventFilter.Get1(j).CurrentValue;
                    moneyUI.UIMoneyProInformation.textValue.text = newValue.ToString();
                }
            }
        }
    }
}
