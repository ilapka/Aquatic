using System.Collections.Generic;
using UnityEngine;

namespace UnityComponents
{
    [RequireComponent(typeof(RubbishInformation))]
    public class PipeRingInformation : MonoBehaviour
    {
        public Transform startPoint;
        public Transform endPoint;
        public RubbishInformation rubbishInstance;
    }
}
