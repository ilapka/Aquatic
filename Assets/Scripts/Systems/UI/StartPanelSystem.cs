using Components;
using Components.Events;
using Data;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems.UI
{
    public class StartPanelSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        
        private readonly EcsFilter<StartPanelComponent> _startPanelFilter = null;
        private readonly EcsFilter<UpdateLevelValueEvent> _updateLevelEvent = null;
        
        public void Init()
        {
            foreach (var i in _updateLevelEvent)
            {
                var currentLevel = _updateLevelEvent.Get1(i).CurrentLevel + 1;
                
                foreach (var j in _startPanelFilter)
                {
                    var startPanelInformation = _startPanelFilter.Get1(j).StartPanelInformation;
                    startPanelInformation.levelValueText.text = $"Level {currentLevel.ToString()}";
                    startPanelInformation.startGameButton.onClick.AddListener(OnStartButtonClick);
                } 
            }
        }

        private void OnStartButtonClick()
        {
            foreach (var i in _startPanelFilter)
            {
                _startPanelFilter.Get1(i).StartPanelInformation.gameObject.SetActive(false);
            }
            
            _world.NewEntity().Get<StartGameEvent>();
        }
    }
}
