using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBullet : MonoBehaviour,IEntity
{
    public ThrowWeapon weapon;
    public int damage;
    public float speed;
    public float time;

    [HideInInspector] public Vector3 direction;

    private float _currentTime;
    
    private void Update()
    {
        if (_currentTime < time)
        {
            _currentTime += Time.deltaTime;

            transform.position += direction * (speed * Time.deltaTime);
        }
        else
        {
            var throwManager = ThrowManager.Instance;
            
            throwManager.AddBullet(gameObject,weapon);
        }
    }

    public EntityType GetEntityType()
    {
        return EntityType.Bullet;
    }

    public void ReShot()
    {
        _currentTime = 0;
        gameObject.SetActive(true);
    }
}
