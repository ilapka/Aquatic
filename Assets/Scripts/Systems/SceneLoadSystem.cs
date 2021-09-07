using Components;
using Components.Events;
using Extension;
using Leopotam.Ecs;
using Types;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Systems
{
    public class SceneLoadSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        
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
                    _world.NewEntity().Get<PlayDarkScreenEvent>().AnimatorKey = DarkScreenAnimatorKeys.ShowDarkScreen;
                    _world.NewEntity().Get<LoadSceneComponent>().AsyncOperation = asyncOperation;
                }
                else
                {
                    asyncOperation.allowSceneActivation = true;
                }
            }

            foreach (var i in _loadSceneComponent)
            {
                var asyncOperation = _loadSceneComponent.Get1(i).AsyncOperation;
                
                if (asyncOperation.isDone)
                {
                    GInvoke.Instance.Delay(() =>
                    {
                        _world.NewEntity().Get<PlayDarkScreenEvent>().AnimatorKey = DarkScreenAnimatorKeys.HideDarkScreen;
                    }, 0.2f);
                    asyncOperation.allowSceneActivation = true;
                    _loadSceneComponent.Destroy();
                }
            }
        }
    }
}
