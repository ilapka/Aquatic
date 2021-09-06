using System;
using System.Collections.Generic;
using UnityComponents;
using UnityEngine;
using UnityEngine.Serialization;

namespace Data
{
    [Serializable]
    public struct LevelStruct
    {
        public LocationInformation levelInformation;
        public RingsGenerationData ringsGenerationSettings;
        public LevelData levelData;
    }
    
    [CreateAssetMenu(fileName = "LevelListData",menuName = "Aquatic/Level List Data")]
    public class LevelListData : ScriptableObject
    {
        public List<LevelStruct> levelList;
    }
}
