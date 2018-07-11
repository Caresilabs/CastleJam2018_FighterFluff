
using UnityEngine;

namespace Assets.Scripts
{
    public class ParticleCleaner : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem particles;

        private void Update()
        {
            if (!particles.isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }
}
