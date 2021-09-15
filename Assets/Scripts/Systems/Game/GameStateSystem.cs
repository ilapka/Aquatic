using Components;
using Components.Events;
using Leopotam.Ecs;
using Managers;
using Types;

namespace Systems.Game
{
    public class GameStateSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        
        private readonly EcsFilter<GameStateComponent> _gameStateFilter = null;
        private readonly EcsFilter<StartGameEvent> _startGameFilter = null;
        private readonly EcsFilter<LevelCompleteEvent> _levelCompleteFilter = null;
        
        public void Init()
        {
            _world.NewEntity().Get<GameStateComponent>();
        }
        
        public void Run()
        {
            foreach (var i in _startGameFilter)
            {
                _world.NewEntity().Get<PlayOneShootFlatEvent>().SoundType = SoundType.StartGame;
                SetGameState(true);
            }

            foreach (var i in _levelCompleteFilter)
            {
                SetGameState(false);
                _world.NewEntity().Get<PlayOneShootFlatEvent>().SoundType = SoundType.Victory;
                _world.NewEntity().Get<PlayConfettiEvent>();
                _world.NewEntity().Get<LevelUpEvent>();
            }
        }

        private void SetGameState(bool isGamePlayProcess)
        {
            foreach (var j in _gameStateFilter)
            {
                _gameStateFilter.Get1(j).IsGamePlayProcess = isGamePlayProcess;
            }
        }
    }
}
