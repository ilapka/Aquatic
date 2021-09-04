using System.Collections.Generic;
using UnityEngine;

namespace UnityComponents
{
    [RequireComponent(typeof(DestroyableObject))]
    public class DestructiblePipeRingInformation : PipeRingInformation
    {
        public DestroyableObject destroyableInstance;
    }
}
