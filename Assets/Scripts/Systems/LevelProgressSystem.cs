using Components;
using Components.Events;
using Leopotam.Ecs;
using Types;
using UnityEngine;

namespace Systems
{
    public class LevelProgressSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<LevelComponent> _levelSettingsFilter = null;
        private readonly EcsFilter<ExplosionDestroyableObjectEvent> _explosionFilter = null;
        
        public void Run()
        {
            foreach (var i in _explosionFilter)
            {
                var ringSettings = _explosionFilter.Get1(i).PipeRingSettings;
                if(ringSettings.ringType == PipeRingType.Default) return;
                
                foreach (var j in _levelSettingsFilter)
                {
                    ref var levelComponent = ref _levelSettingsFilter.Get1(j);
                    levelComponent.CurrentLevelScore += ringSettings.reward;
                    levelComponent.LevelProgress = (float) levelComponent.CurrentLevelScore / levelComponent.LevelData.scoreToWin;

                    _world.NewEntity().Get<AddMoneyEvent>().Value = ringSettings.reward;

                    if (levelComponent.CurrentLevelScore >= levelComponent.LevelData.scoreToWin)
                    {
                        _world.NewEntity().Get<LevelCompleteEvent>();
                    }
                }
            }
        }
    }
}
