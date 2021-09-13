using UnityEngine;

namespace UnityComponents.Information
{
    [RequireComponent(typeof(DestroyableObject))]
    public class DestructiblePipeRingInformation : PipeRingInformation
    {
        public DestroyableObject destroyableInstance;
    }
}
