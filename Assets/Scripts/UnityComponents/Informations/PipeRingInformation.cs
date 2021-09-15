using UnityEngine;

namespace UnityComponents.Information
{
    [RequireComponent(typeof(RubbishInformation))]
    public class PipeRingInformation : MonoBehaviour
    {
        public Transform startPoint;
        public Transform endPoint;
        public RubbishInformation rubbishInstance;
    }
}
