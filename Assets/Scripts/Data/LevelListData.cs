using System.Collections.Generic;
using Information;
using UnityEngine;
using UnityEngine.Serialization;

namespace Data
{
    [CreateAssetMenu(fileName = "LevelListData",menuName = "Aquatic/Level List Data")]
    public class LevelListData : ScriptableObject
    {
        public List<LevelData> levelList;
    }
}
