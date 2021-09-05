using Components;
using Components.Events;
using Leopotam.Ecs;
using Types;
using UnityEngine;

namespace Systems.Player
{
    public class BoatStateSystem : IEcsRunSystem
    {
        private static readonly int ButtonStateKey = Animator.StringToHash("ButtonState");
        private static readonly int ClampsStateKey = Animator.StringToHash("ClampsState");

        private readonly EcsFilter<PlayerComponent> _playerFilter = null;
        private readonly EcsFilter<InputComponent> _inputEventFilter = null;
        private readonly EcsFilter<ExplosionDestroyableObjectEvent> _explosionDestroyableFilter = null;

        public void Run()
        {
            foreach (var i in _playerFilter)
            {
                ref var playerComponent = ref _playerFilter.Get1(i);
                
                foreach (var j in _inputEventFilter)
                {
                    var isTouch = _inputEventFilter.Get1(j).IsTouch;
                    if (isTouch)
                    {
                        foreach (var k in _explosionDestroyableFilter)
                        {
                            var pipeRingType = _explosionDestroyableFilter.Get1(k).PipeRingType;
                            if (pipeRingType != PipeRingType.Default)
                            {
                                playerComponent.InContactWithPipe = true;
                                playerComponent.PlayerInformation.playerAnimator.SetBool(ButtonStateKey, true);
                                playerComponent.PlayerInformation.playerAnimator.SetBool(ClampsStateKey, true);
                            }
                        }
                    }
                    else
                    {
                        playerComponent.InContactWithPipe = false;
                        playerComponent.PlayerInformation.playerAnimator.SetBool(ButtonStateKey, false);
                        playerComponent.PlayerInformation.playerAnimator.SetBool(ClampsStateKey, false);
                    }
                }
            }
        }
    }
}
