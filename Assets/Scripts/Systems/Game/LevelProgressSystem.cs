using Systems.Saving;
using Components;
using Components.Events;
using Leopotam.Ecs;
using Managers;
using Types;
using UnityEngine;

namespace Systems
{
    public class LevelProgressSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<LevelComponent> _levelSettingsFilter = null;
        private readonly EcsFilter<ExplosionDestroyableObjectEvent> _explosionFilter = null;
        private readonly EcsFilter<GameStateComponent> _gameStateFilter = null;

        public void Run()
        {
            foreach (var i in _explosionFilter)
            {
                foreach (var j in _gameStateFilter)
                {
                    if(!_gameStateFilter.Get1(j).IsGamePlayProcess) return;
                }
                
                var ringSettings = _explosionFilter.Get1(i).PipeRingSettings;
                if(ringSettings.ringType == PipeRingType.Default) return;
                
                foreach (var j in _levelSettingsFilter)
                {
                    ref var levelComponent = ref _levelSettingsFilter.Get1(j);
                    levelComponent.CurrentLevelScore += ringSettings.price;
                    var moneyToTransaction = ringSettings.price;
                    if (levelComponent.CurrentLevelScore < 0)
                    {
                        moneyToTransaction = ringSettings.price - levelComponent.CurrentLevelScore;
                        levelComponent.CurrentLevelScore = 0;
                    }
                    levelComponent.LevelProgress = (float) levelComponent.CurrentLevelScore / levelComponent.LevelData.scoreToWin;

                    _world.NewEntity().Get<MoneyTransactionEvent>().Value = moneyToTransaction;

                    if (levelComponent.CurrentLevelScore >= levelComponent.LevelData.scoreToWin)
                    {
                        _world.NewEntity().Get<LevelCompleteEvent>().CurrentLevel = LevelValueSystem.CurrentLevel;
                    }
                }
            }
        }
    }
}
