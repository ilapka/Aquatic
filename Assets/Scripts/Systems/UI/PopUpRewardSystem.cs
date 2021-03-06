using Components;
using Components.Events;
using Data;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems.UI
{
    public class PopUpRewardSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        
        private readonly EcsFilter<CanvasComponent> _canvasFilter = null;
        private readonly EcsFilter<ExplosionDestroyableObjectEvent> _explosionFilter = null;

        public void Run()
        {
            foreach (var i in _explosionFilter)
            {
                var ringSettings = _explosionFilter.Get1(i).PipeRingSettings;

                foreach (var j in _canvasFilter)
                {
                    var popupUIContainer = _canvasFilter.Get1(j).CanvasInformation.popUpTextContainer;
                    var popupInformation =  Object.Instantiate(ringSettings.popupTextPrefab, popupUIContainer);
                    popupInformation.textValue.text = ringSettings.price.ToString();
                    Object.Destroy(popupInformation.gameObject, 5f);
                }
            }
        }
    }
}
