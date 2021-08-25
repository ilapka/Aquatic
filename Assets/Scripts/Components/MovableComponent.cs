using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Components
{
    public struct MovableComponent
    {
        public Transform Transform;
        public Rigidbody Rigidbody;
        public float MaxMoveSpeed;
        public float Acceleration;
        public float CurrentSpeed;
        public bool IsMoving;
    }
}
