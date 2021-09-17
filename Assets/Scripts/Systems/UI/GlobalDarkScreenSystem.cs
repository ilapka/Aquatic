using Components;
using Data;
using Leopotam.Ecs;
using Types;
using UnityComponents;
using UnityComponents.Informations;
using UnityEngine;

namespace Systems.UI
{
    public class GlobalDarkScreenSystem: IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly UIData _uiData = null;

        private static GlobalDarkScreenInformation _globalDarkScreenInstance;
        private static bool _darkScreenIsShown;

        private readonly EcsFilter<GlobalCanvasComponent> _globalCanvasFilter = null;
        private readonly EcsFilter<PlayDarkScreenEvent> _playDarkScreenEvent = null;

        public void Init()
        {
            foreach (var i in _globalCanvasFilter)
            {
                var globalCanvasInformation = _globalCanvasFilter.Get1(i).GlobalCanvasInformation;
                
                if (_globalDarkScreenInstance == null)
                {
                    _globalDarkScreenInstance = Object.Instantiate(_uiData.globalDarkScreenPrefab, globalCanvasInformation.uiContainer);
                }

                if (_darkScreenIsShown)
                {
                    _world.NewEntity().Get<PlayDarkScreenEvent>().AnimatorKey = DarkScreenAnimatorKeys.HideDarkScreen;
                }
            }
        }

        public void Run()
        {
            foreach (var i in _playDarkScreenEvent)
            {
                var animatorKey = _playDarkScreenEvent.Get1(i).AnimatorKey;
                _darkScreenIsShown = animatorKey == DarkScreenAnimatorKeys.ShowDarkScreen;
                _globalDarkScreenInstance.darkScreenAnimator.SetTrigger(animatorKey);
            }
        }
    }
}