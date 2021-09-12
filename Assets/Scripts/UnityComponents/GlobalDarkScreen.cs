using Types;
using UnityEngine;

namespace UnityComponents
{
    public class GlobalDarkScreen : MonoBehaviour
    {
        public Animator darkScreenAnimator;
        private bool _isShown;

        public void OnSceneLoaded()
        {
            if (_isShown)
            {
                Play(DarkScreenAnimatorKeys.HideDarkScreen);
            }
        }

        public void Play(int animatorKey)
        {
            _isShown = animatorKey == DarkScreenAnimatorKeys.ShowDarkScreen;
            darkScreenAnimator.SetTrigger(animatorKey);   
        }
    }
}