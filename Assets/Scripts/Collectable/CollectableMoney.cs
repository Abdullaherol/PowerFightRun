using System;
using System.Security.Cryptography;
using UnityEngine;

public class CollectableMoney : MonoBehaviour
{
    public ParticleSystem particle;
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Player"))
        {
            particle.transform.parent = null;
            particle.Play();
            
            GameManager.Instance.AddMoney(GameManager.Instance.eachMoneyValue);
            Destroy(gameObject);
        }
    }
}