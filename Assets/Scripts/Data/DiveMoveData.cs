using UnityEngine;
using UnityEngine.Serialization;

namespace Data
{
    [CreateAssetMenu(fileName = "DiveMoveData",menuName = "Aquatic/Dive Move Data")]
    public class DiveMoveData : ScriptableObject
    {
        public Vector3 diveDirection;
        public float divingSpeed;
        public float surfacingSpeed;
    }
}
