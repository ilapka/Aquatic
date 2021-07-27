using UnityEngine;
using UnityEngine.Serialization;

namespace Data
{
    [CreateAssetMenu(fileName = "MaterialData",menuName = "Aquatic/Material Data")]
    public class MaterialData : ScriptableObject
    {
        public Material waterMaterial;
        public float speedChangeWaterStrength;
    }
}
