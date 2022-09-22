using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

[ExecuteInEditMode]
public class Collectable : MonoBehaviour
{
    public int health;
    public TMPro.TextMeshProUGUI text;
    public Animator animator;
    public Transform itemParent;

    void Start()
    {
        text.text = health.ToString();
        itemParent.GetComponent<Animator>().Play("CollectableWeapon", -1, Random.value);
    }

    private void Update()
    {
        if (Application.isPlaying) return;

        text.text = health.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            ThrowBullet bullet = other.GetComponent<ThrowBullet>();

            if (health - bullet.damage <= 0)
            {
                itemParent.parent = null;
                itemParent.GetComponent<Animator>().enabled = false;
                itemParent.eulerAngles = new Vector3(itemParent.eulerAngles.x, itemParent.eulerAngles.y, 0);
                itemParent.GetComponent<BoxCollider>().isTrigger = false;
                itemParent.GetComponent<Rigidbody>().useGravity = true;
                Destroy(gameObject);
            }
            else
            {
                health -= bullet.damage;

                animator.Play("Damage");
            }

            bullet.particle.transform.parent = null;
            bullet.particle.Play();
            
            Destroy(bullet.gameObject);
            
            text.text = health.ToString();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
        }
    }
}