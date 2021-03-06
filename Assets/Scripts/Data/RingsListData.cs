using System;
using System.Collections.Generic;
using Types;
using UnityComponents;
using UnityComponents.Informations;
using UnityEngine;
using UnityEngine.Serialization;

namespace Data
{
    [Serializable]
    public struct PipeRingStruct
    {
        public PipeRingType ringType;
        public PipeRingInformation pipeRingInformation;
        public int price;
        public UITextInformation popupTextPrefab;
    }

    [CreateAssetMenu(fileName = "RingsListData", menuName = "Aquatic/Rings List Data")]
    public class RingsListData : ScriptableObject
    {
        public List<PipeRingStruct> pipeRingsList;
    }
}
