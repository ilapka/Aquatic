using Data;
using Extension;
using UnityEngine;

namespace UnityComponents
{
    public class GlobalObjectsContainer : Singleton<GlobalObjectsContainer>
    {
        [HideInInspector] public GlobalCanvas globalCanvas;
        private bool _init;

        public void Init(UIData uiData)
        {
            if(_init) return;
            
            _init = true;
            globalCanvas = Instantiate(uiData.globalCanvasPrefab, transform);
            globalCanvas.Init(uiData);
        }
    }
}