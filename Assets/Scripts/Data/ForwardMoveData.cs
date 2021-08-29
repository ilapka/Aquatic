using UnityEngine;
using UnityEngine.Serialization;

namespace Data
{
    [CreateAssetMenu(fileName = "ForwardMoveData",menuName = "Aquatic/Forward Move Data")]
    public class ForwardMoveData : ScriptableObject
    {
        public float speed;
        public Vector3 direction;
    }
}
