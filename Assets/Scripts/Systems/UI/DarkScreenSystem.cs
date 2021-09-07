using Components;
using Components.Events;
using Data;
using Leopotam.Ecs;
using UnityComponents;
using UnityEngine;

namespace Systems.UI
{
    public class DarkScreenSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly UIData _uiData = null;
        
        private readonly EcsFilter<CanvasComponent> _canvasFilter = null;
        private readonly EcsFilter<DarkScreenComponent> _darkScreenFilter = null;
        private readonly EcsFilter<PlayDarkScreenEvent> _playDarkScreenFilter = null;

        public void Init()
        {
            foreach (var i in _canvasFilter)
            {
                ref var canvasComponent = ref _canvasFilter.Get1(i);
                var darkScreenInformation = Object.Instantiate(_uiData.darkScreenPrefab, canvasComponent.CanvasInformation.uiContainer);
                _canvasFilter.GetEntity(i).Get<DarkScreenComponent>().DarkScreenInformation = darkScreenInformation;
            }
        }

        public void Run()
        {
            foreach (var i in _playDarkScreenFilter)
            {
                var screenKey = _playDarkScreenFilter.Get1(i).AnimatorKey;
                foreach (var j in _darkScreenFilter)
                {
                    var darkScreenInformation = _darkScreenFilter.Get1(j).DarkScreenInformation;
                    darkScreenInformation.darkScreenAnimator.SetTrigger(screenKey);
                }
            }
        }
    }
}
