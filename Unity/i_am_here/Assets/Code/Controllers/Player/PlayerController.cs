using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SoundWaveController sound_wave_controller_ = null;
    [Range(0.0f, 5.0f)] [SerializeField] private float next_burst_time_ = 0.3f;
    [Range(1, 360)] [SerializeField] private int burst_separation_angle_ = 20;
    [Range(1, 360)] [SerializeField] private int bursOffsetAngle = 5;
    [Range(0.0f, 1.0f)] [SerializeField] private float burstOffsetVector = 0.2f;
    [Range(0.0f, 100.0f)] [SerializeField] private float force_strenght_ = 3f;
    [Range(0.0f, 100.0f)] [SerializeField] private float movementCoeficient = 0.1f;
    
    private float burst_timer_ = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        burst_timer_ = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        burst_timer_ += Time.deltaTime;

        bool key_pressed = false;

        
        // Input handling
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * movementCoeficient;
            key_pressed = true;
        }
        
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * movementCoeficient;
            key_pressed = true;
        }
        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.up * movementCoeficient;
            key_pressed = true;
        }
        
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.down * movementCoeficient;
            key_pressed = true;
        }

        // Visual presentation
        if (key_pressed && burst_timer_ > next_burst_time_)
        {
            Burst();
            burst_timer_ = 0.0f;
        }
    }

    void Burst()
    {
        for (int angle = 0; angle < 360; angle += burst_separation_angle_)
        {
            float angle_in_rad = (float) (angle + bursOffsetAngle) * Mathf.Deg2Rad;
            Vector2 dir = new Vector2((float)Math.Cos(angle_in_rad), (float)Math.Sin(angle_in_rad));

            SoundWaveController sound_wave_controller =
                Instantiate(sound_wave_controller_, transform.position + new Vector3(dir.x, dir.y, 0) * burstOffsetVector, Quaternion.identity, null);
            sound_wave_controller.GetRigidbody().AddForce(dir * force_strenght_, ForceMode2D.Impulse);
        }

    }

}
