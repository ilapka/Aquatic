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
        private readonly EcsFilter<LoadSceneComponent> _loadSceneComponent = null;
        
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
                    GlobalObjectsContainer.Instance.globalCanvas.PlayDarkScreen(DarkScreenAnimatorKeys.ShowDarkScreen);
                    GInvoke.Instance.Delay(() =>
                    {
                        _world.NewEntity().Get<LoadSceneComponent>().AsyncOperation = asyncOperation;
                    }, _sceneLoadData.allowLoadDelay);
                }
                else
                {
                    asyncOperation.allowSceneActivation = true;
                }
            }

            foreach (var i in _loadSceneComponent)
            {
                var asyncOperation = _loadSceneComponent.Get1(i).AsyncOperation;
                
                if (asyncOperation.progress >= 0.89f)
                {
                    GInvoke.Instance.Delay(() =>
                    {
                        GlobalObjectsContainer.Instance.globalCanvas.PlayDarkScreen(DarkScreenAnimatorKeys.HideDarkScreen);
                    }, 0.1f);
                    asyncOperation.allowSceneActivation = true;
                    _loadSceneComponent.Destroy();
                }
            }
        }
    }
}
