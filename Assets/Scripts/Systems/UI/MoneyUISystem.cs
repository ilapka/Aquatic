using Components;
using Components.Events;
using Data;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems.UI
{
    public class MoneyUISystem : IEcsRunSystem
    {
        private readonly UIData _uiData = null;

        private readonly EcsFilter<UpdateMoneyValueEvent> _updateMoneyEventFilter = null;
        private readonly EcsFilter<MoneyUIComponent> _uiFilter = null;

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
