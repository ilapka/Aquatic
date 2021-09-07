using System;
using Components;
using Components.Events;
using Data;
using Leopotam.Ecs;
using Types;
using UnityEditor;
using Object = UnityEngine.Object;

namespace Systems.UI
{
    public class CompletePanelSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly UIData _uiData = null;
        
        private readonly EcsFilter<CanvasComponent> _canvasFilter = null;
        private readonly EcsFilter<CompletePanelComponent> _completePanelComponent = null;
        private readonly EcsFilter<LevelCompleteEvent> _levelCompleteFilter = null;
        
        public void Init()
        {
            foreach (var i in _canvasFilter)
            {
                ref var canvasComponent = ref _canvasFilter.Get1(i);
                var completePanelInformation = Object.Instantiate(_uiData.completePanelPrefab, canvasComponent.CanvasInformation.uiContainer);
                completePanelInformation.gameObject.SetActive(false);
                completePanelInformation.resumeButton.onClick.AddListener(OnButtonClick);
                _canvasFilter.GetEntity(i).Get<CompletePanelComponent>().CompletePanelInformation = completePanelInformation;
            }
        }

        private void OnButtonClick()
        {
            foreach (var j in _completePanelComponent)
            {
                _completePanelComponent.Get1(j).CompletePanelInformation.gameObject.SetActive(false);
                var sceneLoadEvent = new LoadSceneEvent()
                {
                    SceneToLoad = SceneType.Game,
                    UseDarkScreen = true,
                };
                _world.NewEntity().Replace(sceneLoadEvent);
            }
        }

        public void Run()
        {
            foreach (var i in _levelCompleteFilter)
            {
                foreach (var j in _completePanelComponent)
                {
                    _completePanelComponent.Get1(j).CompletePanelInformation.gameObject.SetActive(true);
                }
            }
        }
    }
}
