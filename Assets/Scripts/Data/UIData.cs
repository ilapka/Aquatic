using UnityComponents;
using UnityComponents.Informations;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "UIData",menuName = "Aquatic/UI Data")]
    public class UIData : ScriptableObject
    {
        [Header("Common canvas")]
        public CanvasInformation canvasPrefab;
        public float fillingProgressBarSpeed;
        public ProgressBarInformation levelProgressBarPrefab;
        public UITextProInformation playerMoneyTextProPrefab;
        public StartPanelInformation startPanelPrefab;
        public CompletePanelInformation completePanelPrefab;

        [Header("Global canvas")]
        public GlobalCanvasInformation globalCanvasPrefab;
        public GlobalDarkScreenInformation globalDarkScreenPrefab;
    }
}
