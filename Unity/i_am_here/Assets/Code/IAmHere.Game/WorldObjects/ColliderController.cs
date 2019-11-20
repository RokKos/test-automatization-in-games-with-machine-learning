using UnityEngine;

namespace IAmHere.Game
{
    public class ColliderController : WorldEntityController
    {
        protected override void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                onBurst(this, transform.position, gradient, fadeTrails, burstSeparationAngle, bursOffsetAngle, burstOffsetVector, forceStrenght, maxTimeAlive);
                Debug.Log("Player Dead");
            }
        }
    }
}
