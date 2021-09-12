using Data;
using Extension;
using UnityEngine;

namespace UnityComponents
{
    public class GlobalObjectsContainer : Singleton<GlobalObjectsContainer>
    {
        [HideInInspector] public GlobalCanvas globalCanvas;
        private bool _init;

        public void OnSceneLoaded(UIData uiData)
        {
            if (!_init)
            {
                globalCanvas = Instantiate(uiData.globalCanvasPrefab, transform);
                _init = true;
            }

            globalCanvas.OnSceneLoaded(uiData);
        }
    }
}