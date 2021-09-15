using System.Collections.Generic;
using Types;
using UnityEngine;
using UnityEngine.Events;

namespace UnityComponents.Informations
{
    [RequireComponent(typeof(Collider))]
    public class DestroyableObject : MonoBehaviour
    {
        [HideInInspector] public UnityEvent<DestroyableObject, Collider> triggerEvent;
        
        public TagEnum triggerTag;
        public SoundType destroySound;
        public float explosionForce;
        public float explosionRadius;
        public float bodyPartLifeTime = 5f;
        public List<Rigidbody> bodyParts;

        private void OnTriggerEnter(Collider other)
        {
            triggerEvent.Invoke(this, other);
        }
        
        private void OnDestroy()
        {
            triggerEvent.RemoveAllListeners();
        }
    }
}
