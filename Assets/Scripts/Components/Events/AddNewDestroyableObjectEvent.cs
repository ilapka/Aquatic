using System.Collections.Generic;
using Data;
using Types;
using UnityComponents;

namespace Components.Events
{
    public struct AddNewDestroyableObjectEvent
    {
        public DestroyableObject DestroyableObjects;
        public PipeRingStruct PipeRingSettings;
    }
}
