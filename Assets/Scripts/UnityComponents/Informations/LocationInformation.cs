using UnityEngine;

namespace UnityComponents.Informations
{
    public class LocationInformation : MonoBehaviour
    {
        public Transform playerSpawnPoint;
        public Transform pipeRingsContainer;
        [Header("Extendable environment parts")]
        public Transform firstEnvironmentPart;
        public Transform secondEnvironmentPart;
    }
}
