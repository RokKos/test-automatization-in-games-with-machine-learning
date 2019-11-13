using System;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class WorldEntityController : MonoBehaviour
{
    [Range(1, 360)] [SerializeField] protected int burstSeparationAngle = 20;
    [Range(1, 360)] [SerializeField] protected int bursOffsetAngle = 5;
    [Range(0.0f, 1.0f)] [SerializeField] protected float burstOffsetVector = 0.2f;
    [Range(0.0f, 100.0f)] [SerializeField] protected float forceStrenght = 3f;
    [Range(0.0f, 100.0f)] [SerializeField] protected float movementCoeficient = 0.1f;

    protected SoundWaveController soundWaveController = null;

    public void Init(SoundWaveController _soundWaveController)
    {
        soundWaveController = _soundWaveController;
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        throw new NotImplementedException();
    }
    
    protected virtual void Burst(Color color)
    {
        for (int angle = 0; angle < 360; angle += burstSeparationAngle)
        {
            float angleInRad = (float) (angle + bursOffsetAngle) * Mathf.Deg2Rad;
            Vector2 dir = new Vector2((float)Math.Cos(angleInRad), (float)Math.Sin(angleInRad));

            SoundWaveController sound_wave_controller =
                Instantiate(soundWaveController, transform.position + new Vector3(dir.x, dir.y, 0) * burstOffsetVector, Quaternion.identity, null);
            sound_wave_controller.GetRigidbody().AddForce(dir * forceStrenght, ForceMode2D.Impulse);
            sound_wave_controller.SetLineColor(color);
        }

    }
}
