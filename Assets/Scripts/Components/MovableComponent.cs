using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Components
{
    public struct MovableComponent
    {
        public Transform transform;
        public Rigidbody rigidbody;
        public float maxMoveSpeed;
        public float acceleration;
        public float currentSpeed;
        public bool isMoving;
    }
}
