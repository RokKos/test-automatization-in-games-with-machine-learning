using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWaveController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody_ = null;
    [SerializeField] private CircleCollider2D collider_ = null;
    [SerializeField] private float max_time_alive_ = 1.0f;
    [SerializeField] private TrailRenderer TrailRenderer = null;

    private float time_alive_ = 0.0f;

    public Rigidbody2D GetRigidbody()
    {
        return rigidbody_;
    }

    public CircleCollider2D GetCircleCollider()
    {
        return collider_;

    }

    private void Start()
    {
        time_alive_ = 0.0f;
    }

    private void Update()
    {
        time_alive_ += Time.deltaTime;
        if (time_alive_ > max_time_alive_)
        {
            // TODO(Rok Kos): Pooling
            Destroy(this.gameObject);
        }
    }

    public void SetLineColor(Color color)
    {
        TrailRenderer.startColor = color;
        TrailRenderer.endColor = color;
    }


}
