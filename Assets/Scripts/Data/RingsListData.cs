using System;
using System.Collections.Generic;
using Types;
using UnityComponents;
using UnityEngine;

namespace Data
{
    [Serializable]
    public struct PipeRingStruct
    {
        public PipeContentType contentType;
        public PipeRingInformation pipeRingInformation;
        public int reward;
        public UITextInformation popupTextPrefab;
    }

    [CreateAssetMenu(fileName = "RingsListData", menuName = "Aquatic/Rings List Data")]
    public class RingsListData : ScriptableObject
    {
        public List<PipeRingStruct> pipeRingsList;
    }
}
