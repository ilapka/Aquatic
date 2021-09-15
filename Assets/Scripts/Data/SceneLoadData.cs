using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "SceneLoadData", menuName = "Aquatic/Scene Load Data", order = 0)]
    public class SceneLoadData : ScriptableObject
    {
        public float allowLoadDelay;
    }
}