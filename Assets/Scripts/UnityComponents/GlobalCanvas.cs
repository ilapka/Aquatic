using Data;
using UnityEngine;

namespace UnityComponents
{
    public class GlobalCanvas : MonoBehaviour
    {
        private DarkScreenInformation _darkScreenInformation;
        private bool _init;

        public void Init(UIData uiData)
        {
            if(_init) return;
            
            _init = true;
            _darkScreenInformation = Instantiate(uiData.darkScreenPrefab, transform);
        }

        public void PlayDarkScreen(int animatorKey)
        {
            _darkScreenInformation.darkScreenAnimator.SetTrigger(animatorKey);   
        }
    }
}
