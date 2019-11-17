using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace IAmHere.Game
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class WorldEntityController : MonoBehaviour
    {
        public delegate void OnBurst(WorldEntityController entity, Vector3 position, Color color, int burstSeparationAngle, int bursOffsetAngle, float burstOffsetVector,
            float forceStrenght, float maxTimeAlive);
        public OnBurst onBurst;
        
        [Range(1, 360)] [SerializeField] protected int burstSeparationAngle = 20;
        [Range(1, 360)] [SerializeField] protected int bursOffsetAngle = 5;
        [Range(0.0f, 1.0f)] [SerializeField] protected float burstOffsetVector = 0.2f;
        [Range(0.0f, 100.0f)] [SerializeField] protected float forceStrenght = 3f;
        [Range(0.0f, 10.0f)] [SerializeField] protected float maxTimeAlive = 1.0f;


        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            throw new NotImplementedException();
        }
    }
}