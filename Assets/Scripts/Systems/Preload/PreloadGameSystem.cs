using Components.Events;
using Leopotam.Ecs;
using Types;
using UnityComponents.Emitters;
using UnityEngine;

namespace Systems.Preload
{
    public class PreloadGameSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly PreloadUIEmitter _preloadUIEmitter = null;
        
        private readonly EcsFilter<SceneLoadingComponent> _sceneLoadingComponent = null;

        
        public void Init()
        {
            _preloadUIEmitter.preloadBarFill.fillAmount = 0f;
            _world.NewEntity().Get<LoadSceneEvent>().SceneToLoad = SceneType.Game;
        }
        
        public void Run()
        {
            foreach (var i in _sceneLoadingComponent)
            {
                _preloadUIEmitter.preloadBarFill.fillAmount = _sceneLoadingComponent.Get1(i).AsyncOperation.progress + 0.11f;
            }
        }
    }
}
