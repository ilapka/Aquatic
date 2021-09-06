using Components;
using Components.Events;
using Data;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems.Saving
{
    public sealed class WalletSystem : IEcsInitSystem ,IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly GameProgressSavedData _gameProgressData = null;
        
        private readonly EcsFilter<SpendMoneyEvent> _spendMoneyFilter = null;
        private readonly EcsFilter<AddMoneyEvent> _addMoneyFilter = null;

        public void Init()
        {
            _world.NewEntity().Get<UpdateMoneyValueEvent>().CurrentValue = _gameProgressData.playerMoney;
        }
        
        public void Run()
        {
            foreach (var i in _spendMoneyFilter)
            {
                _gameProgressData.playerMoney -= _spendMoneyFilter.Get1(i).Value;
                _world.NewEntity().Get<UpdateMoneyValueEvent>().CurrentValue = _gameProgressData.playerMoney;
            }
            
            foreach (var i in _addMoneyFilter)
            {
                _gameProgressData.playerMoney += _addMoneyFilter.Get1(i).Value;
                _world.NewEntity().Get<UpdateMoneyValueEvent>().CurrentValue = _gameProgressData.playerMoney;
            }
        }
    }
}