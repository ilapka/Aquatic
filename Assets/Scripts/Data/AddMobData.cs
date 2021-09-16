using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "AddMobData", menuName = "Aquatic/SDK/AddMobData", order = 0)]
    public class AddMobData : ScriptableObject
    {
        public string bannerId = "ca-app-pub-3940256099942544/6300978111";
        public float showInterstitialDelay = 0.5f;
        public string interstitialId = "ca-app-pub-3940256099942544/1033173712";
    }
}