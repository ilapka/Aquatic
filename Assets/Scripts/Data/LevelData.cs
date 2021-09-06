using UnityEngine;
using UnityEngine.Serialization;

namespace Data
{
    [CreateAssetMenu(fileName = "LevelData",menuName = "Aquatic/Level Data")]
    public class LevelData : ScriptableObject
    {
        public int scoreToWin;
    }
}
