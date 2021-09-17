using Systems.Saving;
using Components;
using Components.Events;
using Data;
using Leopotam.Ecs;
using Managers;
using Types;
using UnityEngine;

namespace Systems.UI
{
    public class StartPanelSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        private readonly UIData _uiData = null;
        
        private readonly EcsFilter<CanvasComponent> _canvasFilter = null;
        private readonly EcsFilter<StartPanelComponent> _startPanelFilter = null;
        private readonly EcsFilter<CreateLevelEvent> _createLevelEvent = null;
        
        public void Init()
        {
            foreach (var i in _createLevelEvent)
            {
                var currentLevel = _createLevelEvent.Get1(i).LevelValue + 1;
                
                foreach (var j in _canvasFilter)
                {
                    ref var canvasComponent = ref _canvasFilter.Get1(i);
                    var startPanelInformation = Object.Instantiate(_uiData.startPanelPrefab, canvasComponent.CanvasInformation.uiContainer);
                    startPanelInformation.levelValueText.text = $"Level {currentLevel.ToString()}";
                    startPanelInformation.startGameButton.onClick.AddListener(OnStartButtonClick);
                    _canvasFilter.GetEntity(i).Get<StartPanelComponent>().StartPanelInformation = startPanelInformation;
                } 
            }
        }

        private void OnStartButtonClick()
        {
            _world.NewEntity().Get<PlayOneShootFlatEvent>().SoundType = SoundType.ButtonClick;
            
            foreach (var i in _startPanelFilter)
            {
                _startPanelFilter.Get1(i).StartPanelInformation.gameObject.SetActive(false);
            }

            _world.NewEntity().Get<StartGameEvent>().CurrentLevel = LevelValueSystem.CurrentLevel;
        }
    }
}
