using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float collisionSpeed = -2;
    public float borderX;
    public Vector2 lerp;
    public Vector3 speed;
    public GameObject camera;
    public Vector3 cameraOffset;
    
    private Vector2 _lastMousePosition;

    private float _speedZ;
    private bool _failed;
    private bool _gameStarted;

    private void Start()
    {
        GameManager.Instance.OnFailed += OnFailed;
        GameManager.Instance.OnGameStarted += OnGameStarted;
        
        camera.transform.position =
            new Vector3(0, transform.position.y + cameraOffset.y, transform.position.z + cameraOffset.z);
    }

    private void OnFailed()
    {
        _failed = true;
    }
    private void OnGameStarted()
    {
        _gameStarted = true;
    }

    void Update()
    {
        if(_failed || !_gameStarted) return;
        
        var nextPosition = new Vector3(0,transform.position.y,_speedZ);
        
        if (Input.GetMouseButtonDown(0))
        {
            _lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            // _speedZ = Mathf.Clamp(_speedZ + Time.deltaTime, collisionSpeed, speed.z);
            _speedZ = speed.z;
            
            var mousePos = (Vector2)Input.mousePosition;
            var dif = mousePos - _lastMousePosition;

            if (dif.x < 0)
                nextPosition.x = -speed.x;
            else if (dif.x > 0)
                nextPosition.x = speed.x;
            
            _lastMousePosition = Input.mousePosition;
            
            PlayerManager.Instance.PlayRunAnim();
            
            ThrowManager.Instance.Shot(true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _speedZ = 0;
            PlayerManager.Instance.PlayIdleAnim();
            ThrowManager.Instance.Shot(false);
        }

        nextPosition.x = Mathf.Clamp(transform.position.x + nextPosition.x, -borderX, borderX);

        transform.position = new Vector3(Mathf.Lerp(transform.position.x,nextPosition.x,lerp.x * Time.deltaTime)
            ,nextPosition.y
            ,Mathf.Lerp(transform.position.z,transform.position.z+ nextPosition.z,lerp.y * Time.deltaTime));

        camera.transform.position =
            new Vector3(0, transform.position.y + cameraOffset.y, transform.position.z + cameraOffset.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("CollectableCube"))
        {
            _speedZ = collisionSpeed;
        }
    }
}
