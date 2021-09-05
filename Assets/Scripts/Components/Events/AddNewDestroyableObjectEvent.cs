using System.Collections.Generic;
using Types;
using UnityComponents;

namespace Components.Events
{
    public struct AddNewDestroyableObjectEvent
    {
        public DestroyableObject DestroyableObjects;
        public PipeRingType PipeRingType;
    }
}
