using Components;
using Components.Events;
using Data;
using Leopotam.Ecs;

namespace Systems.Saving
{
    public sealed class LevelValueSystem : IEcsInitSystem ,IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly GameProgressSavedData _gameProgressData = null;

        private readonly EcsFilter<LevelUpEvent> _levelUpFilter = null;
        private readonly EcsFilter<SavingComponent> _savingFilter = null;

        public void Init()
        {
            var startEntity = _world.NewEntity();
            startEntity.Get<StartGameEvent>(); //TODO <------------------- переместить в гейм стейт систему, работающую с UI
            startEntity.Get<UpdateLevelValueEvent>().CurrentLevel = _gameProgressData.levelValue;
        }
        
        public void Run()
        {
            foreach (var i in _levelUpFilter)
            {
                _gameProgressData.levelValue++;
                foreach (var j in _savingFilter)
                {
                    _savingFilter.GetEntity(j).Get<SaveDataEvent>();
                }
                _world.NewEntity().Get<UpdateLevelValueEvent>().CurrentLevel = _gameProgressData.levelValue;
            }
        }
    }
}