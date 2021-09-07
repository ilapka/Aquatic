using UnityEngine;

namespace UnityComponents
{
    public class PlayerInformation : MonoBehaviour
    {
        public Transform playerContainerTransform;
        public Transform playerBoatTransform;
        public Animator playerAnimator;
        [Header("Player particles")]
        public ParticleSystem confettiParticle;
    }
}
