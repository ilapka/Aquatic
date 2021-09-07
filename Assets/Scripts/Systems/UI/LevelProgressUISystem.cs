using System;
using Components;
using Components.Events;
using Data;
using Leopotam.Ecs;
using UnityComponents;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Systems.UI
{
    public class LevelProgressUISystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly UIData _uiData = null;

        private readonly EcsFilter<CanvasComponent> _canvasFilter = null;
        private readonly EcsFilter<LevelProgressBarUIComponent> _uiFilter = null;
        private readonly EcsFilter<LevelComponent> _levelFilter = null;
        private readonly EcsFilter<UpdateMoneyValueEvent> _updateMoneyFilter = null;
        
        public void Init()
        {
            foreach (var i in _canvasFilter)
            {
                ref var canvasComponent = ref _canvasFilter.Get1(i);
                var levelProgressBarInformation = Object.Instantiate(_uiData.levelProgressBarPrefab, canvasComponent.CanvasInformation.uiContainer);
                _canvasFilter.GetEntity(i).Get<LevelProgressBarUIComponent>().LevelProgressBarInformation = levelProgressBarInformation;
            }
        }
        
        public void Run()
        {
            foreach (var i in _uiFilter)
            {
                ref var progressBarComponent = ref _uiFilter.Get1(i);
                ref var progressBar = ref progressBarComponent.LevelProgressBarInformation;
                
                if (progressBar == null) return;

                foreach (var j in _updateMoneyFilter)
                {
                    progressBarComponent.targetFillAmount = GetLevelProgress();
                }

                if (Math.Abs(progressBar.fill.fillAmount - progressBarComponent.targetFillAmount) > 0.001f)
                {
                    progressBar.fill.fillAmount = Mathf.Lerp(progressBar.fill.fillAmount, progressBarComponent.targetFillAmount,
                        _uiData.fillingProgressBarSpeed * Time.deltaTime);
                }
            }
        }

        private float GetLevelProgress()
        {
            foreach (var i in _levelFilter)
            {
                return _levelFilter.Get1(i).LevelProgress;
            }

            return 0f;
        }
    }
}
