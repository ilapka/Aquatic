using Systems;
using Systems.Preload;
using Components.Events;
using Data;
using Leopotam.Ecs;
using UnityComponents.Emitters;
using UnityEngine;

namespace Starters
{
    public class PreloadStarter : MonoBehaviour
    {
        private EcsWorld _world;
        private EcsSystems _updateSystems;
    
        [Header("Data")]
        [SerializeField] private SceneLoadData sceneLoadData;

    
        [Header("Emitters")]
        [SerializeField] private PreloadUIEmitter preloadUIEmitter;

        private void Start()
        {
            _world = new EcsWorld();
            _updateSystems = new EcsSystems(_world);


#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_updateSystems);
#endif

            _updateSystems
                .Add(new PreloadGameSystem())
                .Add(new SceneLoadSystem())
            
                .Inject(preloadUIEmitter)
                .Inject(sceneLoadData)
            
                .OneFrame<LoadSceneEvent>()
            
                .Init();
        }
    
    
        private void Update()
        {
            _updateSystems.Run();
        }
    
        private void OnDestroy()
        {
            _updateSystems.Destroy();
            _world.Destroy();
        }
    }
}
