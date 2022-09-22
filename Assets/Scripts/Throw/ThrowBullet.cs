using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBullet : MonoBehaviour
{
    public int damage;
    public float speed;
    public float time;
    public ParticleSystem particle;

    private float _currentTime;
    
    private void Update()
    {
        if (_currentTime < time)
        {
            _currentTime += Time.deltaTime;

            transform.position += transform.forward * (speed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
