using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "PipeGenerationData",menuName = "Aquatic/Pipe Generation Data")]
    public class PipeGenerationData : ScriptableObject
    {
        public float generateDistance;
        //Как генерировать трубу из каких ресурсов в каких пропорциях
    }
}
