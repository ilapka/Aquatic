using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UnityComponents.Information
{
    [RequireComponent(typeof(Collider))]
    public class DestroyableObject : MonoBehaviour
    {
        [HideInInspector] public UnityEvent<DestroyableObject, Collider> triggerEvent;
        
        public TagEnum triggerTag;
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
