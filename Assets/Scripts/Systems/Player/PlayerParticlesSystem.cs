using Components;
using Components.Events;
using Leopotam.Ecs;

namespace Systems.Player
{
    public class PlayerParticlesSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<PlayerComponent> _playerFilter = null;
        private readonly EcsFilter<PlayConfettiEvent> _playConfettiFilter = null;

        public void Run()
        {
            foreach (var i in _playerFilter)
            {
                ref var playerInformation = ref _playerFilter.Get1(i).PlayerInformation;

                foreach (var j in _playConfettiFilter)
                {
                    playerInformation.confettiParticle.Play();
                }
                
                
                
            }
        }
    }
}
