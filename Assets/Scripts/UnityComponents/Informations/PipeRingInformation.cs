using UnityEngine;

namespace UnityComponents.Informations
{
    [RequireComponent(typeof(RubbishInformation))]
    public class PipeRingInformation : MonoBehaviour
    {
        public Transform startPoint;
        public Transform endPoint;
        public RubbishInformation rubbishInstance;
    }
}
