using Components;
using Leopotam.Ecs;

namespace Systems.Input
{
    public sealed class InputSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<InputComponent> _inputFilter = null;
        private readonly EcsFilter<GameStateComponent> _gameStateFilter = null;
        
        public void Init()
        {
            _world.NewEntity().Get<InputComponent>();
        }

        public void Run()
        {
            foreach (var i in _gameStateFilter)
            {
                if(!_gameStateFilter.Get1(i).IsGamePlayProcess) return;;
                
                foreach (var j in _inputFilter)
                {
                    ref var inputComponent = ref _inputFilter.Get1(j);
                    inputComponent.IsTouch = UnityEngine.Input.GetMouseButton(0);
                }
            }
        }
    }
}
