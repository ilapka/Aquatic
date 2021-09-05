using Components.Events;
using Leopotam.Ecs;
using Types;
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
                var pipeRingType = _addNewObjectFilter.Get1(i).PipeRingType;
                destroyableObject.triggerEvent.AddListener((DestroyableObject destroyableObj, Collider collider) =>
                    {
                        TriggerEnter(destroyableObj, collider, pipeRingType);
                    }
                );
            }
        }

        private void TriggerEnter(DestroyableObject destroyableObject, Collider collider, PipeRingType pipeRingType)
        {
            if (collider.transform.CompareTag(destroyableObject.triggerTag.ToString()))
            {
                foreach (var bodyPart in destroyableObject.bodyParts)
                {
                    if(bodyPart == null) continue;
                    
                    bodyPart.transform.parent = null;
                    Object.Destroy(bodyPart.gameObject, destroyableObject.bodyPartLifeTime);
                    
                    bodyPart.isKinematic = false;

                    _world.NewEntity().Get<ExplosionDestroyableObjectEvent>().PipeRingType = pipeRingType;
                }

                Object.Destroy(destroyableObject.gameObject);
            }
        }
    }
}