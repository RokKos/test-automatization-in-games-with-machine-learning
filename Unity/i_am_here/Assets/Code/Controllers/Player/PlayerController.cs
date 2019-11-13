using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : WorldEntityController
{
    
    [Range(0.0f, 5.0f)] [SerializeField] private float nextBurstTime = 0.3f;

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
        if (key_pressed && burst_timer_ > nextBurstTime)
        {
            Burst(Color.white);
            burst_timer_ = 0.0f;
        }
    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Goal")
        {
            // Handled in GoalController
            return;
        }


        if (other.gameObject.tag == "Enviroment")
        {
            Burst(Color.red);
        }
    }

}
