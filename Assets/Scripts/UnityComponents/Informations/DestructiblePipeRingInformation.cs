using UnityComponents.Informations;
using UnityEngine;

namespace UnityComponents.Informations
{
    [RequireComponent(typeof(DestroyableObject))]
    public class DestructiblePipeRingInformation : PipeRingInformation
    {
        public DestroyableObject destroyableInstance;
    }
}
