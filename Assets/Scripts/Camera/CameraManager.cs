using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;
    
    void Update()
    {
        transform.position = new Vector3(0, transform.position.y, transform.position.z);
    }
}
