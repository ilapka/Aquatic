using UnityEngine;
using UnityEngine.Serialization;

namespace Data
{
    [CreateAssetMenu(fileName = "PlayerData",menuName = "Aquatic/Player Data")]
    public class PlayerData : ScriptableObject
    {
        public GameObject playerPrefab;
        public float maxSpeed;
        public float acceleration;
        public Vector3 spawnPosition;
    }
}
