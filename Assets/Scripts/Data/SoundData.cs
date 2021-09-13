using System;
using System.Collections.Generic;
using Types;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

namespace Data
{
    [Serializable]
    public struct SoundStruct
    {
        public SoundType soundType;
        public AudioClip audioClip;
    }

    [CreateAssetMenu(fileName = "SoundData", menuName = "Aquatic/Sound Data", order = 0)]
    public class SoundData : ScriptableObject
    {
        public AudioMixer audioMixer;
        public List<SoundStruct> soundList;
    }
}