using Components;
using Components.Events;
using Data;
using Extension;
using Leopotam.Ecs;
using Types;
using UnityComponents;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Systems
{
    public class SceneLoadSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly SceneLoadData _sceneLoadData = null;

        private readonly EcsFilter<LoadSceneEvent> _loadSceneFilter = null;

        public void Run()
        {
            foreach (var i in _loadSceneFilter)
            {
                var loadSceneEvent = _loadSceneFilter.Get1(i);
                var sceneName = loadSceneEvent.SceneToLoad.ToString();
                var asyncOperation = SceneManager.LoadSceneAsync(sceneName);
                asyncOperation.allowSceneActivation = false;
                
                if (loadSceneEvent.UseDarkScreen)
                {
                    GlobalObjectsContainer.Instance.globalCanvas.darkScreen.Play(DarkScreenAnimatorKeys.ShowDarkScreen);
                }
                
                GInvoke.Instance.Delay(() =>
                {
                    asyncOperation.allowSceneActivation = true;
                }, _sceneLoadData.allowLoadDelay);
                
                _world.NewEntity().Get<SceneLoadingComponent>().AsyncOperation = asyncOperation;
            }
        }
    }
}
