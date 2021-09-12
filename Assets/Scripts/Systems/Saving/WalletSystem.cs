using System;
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
        
        private readonly EcsFilter<MoneyTransactionEvent> _moneyTransactionEvent = null;

        public void Init()
        {
            _world.NewEntity().Get<UpdateMoneyValueEvent>().CurrentValue = _gameProgressData.playerMoney;
        }
        
        public void Run()
        {
            foreach (var i in _moneyTransactionEvent)
            {
                if (_gameProgressData.playerMoney + _moneyTransactionEvent.Get1(i).Value < 0)
                {
                    throw new Exception("Not enough money. You can check the current amount of money with UpdateMoneyValueEvent");
                }

                _gameProgressData.playerMoney += _moneyTransactionEvent.Get1(i).Value;
                _world.NewEntity().Get<UpdateMoneyValueEvent>().CurrentValue = _gameProgressData.playerMoney;
            }
        }
    }
}