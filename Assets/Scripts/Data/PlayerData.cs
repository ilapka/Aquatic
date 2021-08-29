using Information;
using UnityEngine;
using UnityEngine.Serialization;

namespace Data
{
    [CreateAssetMenu(fileName = "PlayerData",menuName = "Aquatic/Player Data")]
    public class PlayerData : ScriptableObject
    {
        public PlayerInformation playerInformationPrefab;
        public ForwardMoveData playerForwardMoveData;
        public DiveMoveData playerDiveMoveData;
    }
}
