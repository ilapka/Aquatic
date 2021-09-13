using Components.Events;
using Data;
using Leopotam.Ecs;
using Types;
using UnityComponents;
using UnityComponents.Information;
using UnityEngine;

namespace Systems.PipeRing
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
                var pipeRingSettings = _addNewObjectFilter.Get1(i).PipeRingSettings;
                destroyableObject.triggerEvent.AddListener((DestroyableObject destroyableObj, Collider collider) =>
                    {
                        TriggerEnter(destroyableObj, collider, pipeRingSettings);
                    }
                );
            }
        }

        private void TriggerEnter(DestroyableObject destroyableObject, Collider collider, PipeRingStruct pipeRingSettings)
        {
            if (collider.transform.CompareTag(destroyableObject.triggerTag.ToString()))
            {
                foreach (var bodyPart in destroyableObject.bodyParts)
                {
                    if(bodyPart == null) continue;
                    
                    bodyPart.transform.parent = null;
                    Object.Destroy(bodyPart.gameObject, destroyableObject.bodyPartLifeTime);
                    
                    bodyPart.isKinematic = false;
                }

                _world.NewEntity().Get<ExplosionDestroyableObjectEvent>().PipeRingSettings = pipeRingSettings;

                Object.Destroy(destroyableObject.gameObject);
            }
        }
    }
}