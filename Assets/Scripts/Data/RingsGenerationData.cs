using System;
using System.Collections.Generic;
using Types;
using UnityEngine;

namespace Data
{
    [Serializable]
    public struct GenerationRingSetting
    {
        public PipeRingType pipeRingType;
        [Range(0f, 10f)]
        public float weightOfChanceToSpawn;
        public int maxCountInRow;
        public int minCountInRow;
    }

    [CreateAssetMenu(fileName = "RingsGenerationData",menuName = "Aquatic/Rings Generation Data")]
    public class RingsGenerationData : ScriptableObject
    {
        public RingsListData ringsList;
        public float generationDistanceFromPlayer;
        public List<GenerationRingSetting> generationRingSettings;
    }
}
