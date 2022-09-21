using System;
using UnityEngine;

public class CollectableWeapon : MonoBehaviour
{
    public ThrowWeapon weapon;

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Player"))
        {
            ThrowManager.Instance.ChangeWeapon(this);
            Destroy(gameObject);
        }
    }
}