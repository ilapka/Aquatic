using Information;
using UnityEngine;
using UnityEngine.Serialization;

namespace Data
{
    [CreateAssetMenu(fileName = "LevelData",menuName = "Aquatic/Level Data")]
    public class LevelData : ScriptableObject
    {
        public LocationInformation locationInformation;
        
        //какие ресурсы спавнить и в какой вероятности + сложность + любые настройки уровня
    }
}
