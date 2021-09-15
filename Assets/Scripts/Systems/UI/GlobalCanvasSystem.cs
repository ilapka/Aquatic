using Components;
using Data;
using Leopotam.Ecs;
using UnityComponents;
using UnityComponents.Informations;
using Object = UnityEngine.Object;

namespace Systems.UI
{
    public class GlobalCanvasSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        private readonly UIData _uiData = null;

        private static GlobalCanvasInformation _globalCanvasInstance;

        public void Init()
        {
            if (_globalCanvasInstance == null)
            {
                _globalCanvasInstance = Object.Instantiate(_uiData.globalCanvasPrefab);
                Object.DontDestroyOnLoad(_globalCanvasInstance);
            }
            
            var uiEntity = _world.NewEntity();
            uiEntity.Get<GlobalCanvasComponent>().GlobalCanvasInformation = _globalCanvasInstance;
        }
    }
}
