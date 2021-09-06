using UnityComponents;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "UIData",menuName = "Aquatic/UI Data")]
    public class UIData : ScriptableObject
    {
        public CanvasInformation canvasPrefab;
        public float fillingProgressBarSpeed;
        public ProgressBarInformation levelProgressBarPrefab;
        public UITextProInformation playerMoneyTextProPrefab;
    }
}
