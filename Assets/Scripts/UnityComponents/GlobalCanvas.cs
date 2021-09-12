using System;
using Data;
using Types;
using UnityEngine;

namespace UnityComponents
{
    public class GlobalCanvas : MonoBehaviour
    {
        [HideInInspector] public GlobalDarkScreen darkScreen;
        private bool _init;

        public void OnSceneLoaded(UIData uiData)
        {
            if (!_init)
            {
                darkScreen = Instantiate(uiData.globalDarkScreenPrefab, transform);
                _init = true;
            }
            
            darkScreen.OnSceneLoaded();
        }
    }
}
