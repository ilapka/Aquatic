using UnityComponents;
using UnityComponents.Information;
using UnityEngine;
using UnityEngine.Serialization;

namespace Data
{
    [CreateAssetMenu(fileName = "PlayerBoatData",menuName = "Aquatic/Player Boat Data")]
    public class PlayerBoatData : ScriptableObject
    {
        public PlayerInformation playerInformationPrefab;
        public ForwardMoveData playerForwardMoveData;
        public DiveMoveData playerDiveMoveData;
    }
}
