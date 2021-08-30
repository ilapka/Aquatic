using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Components
{
    public struct MovableComponent
    {
        public Transform Transform;
        public Vector3 MoveOffset;
        public bool LocalSpaceMoving;
    }
}
