using Components.Events;
using Leopotam.Ecs;
using UnityComponents;
using UnityEngine;

namespace Systems
{
    public sealed class DestroyableObjectsSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<AddNewDestroyableObjectEvent> _addNewObjectFilter = null;
        
        public void Run ()
        {
            foreach (var i in _addNewObjectFilter)
            {
                var destroyableObject = _addNewObjectFilter.Get1(i).DestroyableObjects;
                destroyableObject.triggerEvent.AddListener(OnTrigger);
            }
        }

        private void OnTrigger(DestroyableObject destroyableObject, Collider collider)
        {
            Debug.Log($"On trigger with - {collider.transform.tag}");
            if (collider.transform.CompareTag(destroyableObject.triggerTag.ToString()))
            {
                foreach (var bodyPart in destroyableObject.bodyParts)
                {
                    if(bodyPart == null) continue;
                    
                    bodyPart.transform.parent = null;
                    Object.Destroy(bodyPart.gameObject, 5f);
                    
                    bodyPart.isKinematic = false;
                }

                Object.Destroy(destroyableObject.gameObject);
            }
        }
    }
}