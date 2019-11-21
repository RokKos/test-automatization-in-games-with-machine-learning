using UnityEngine;

namespace IAmHere.Game
{

    public enum MovingState
    {
        kIdle,
        kMoved,
        kHurt,
        kLast
    }
    
    public class MovingEntityController : WorldEntityController
    {
        [Range(0.0f, 5.0f)] [SerializeField] protected float nextBurstTime = 0.3f;
        [Range(0.0f, 100.0f)] [SerializeField] protected float movementCoeficient = 0.1f;

        protected float burstTimer = 0.0f;

        protected MovingState state = MovingState.kIdle;

        public void Init()
        {
            burstTimer = 0.0f;
        }

        protected void ReleaseBurst()
        {
            onBurst(this, transform.position, gradient, fadeTrails, burstSeparationAngle, bursOffsetAngle, burstOffsetVector, forceStrenght, maxTimeAlive);
            burstTimer = 0.0f;
        }
    }
}