using System;
using System.Collections;
using UnityEngine;

[SelectionBase]
public class Enemy : MonoBehaviour
{
    public int health;
    public TMPro.TextMeshProUGUI text;
    public Animator animator;
    public GameObject money;
    public ParticleSystem particleDead;
    public ParticleSystem particleDestroy;

    void Start()
    {
        text.text = health.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            ThrowBullet bullet = other.GetComponent<ThrowBullet>();

            if (health - bullet.damage <= 0)
            {
                animator.Play("Dead");

                particleDead.transform.parent = null;
                particleDead.Play();
                
                this.enabled = false;

                StartCoroutine(DeleteEnemey());
            }
            else
            {
                health -= bullet.damage;
                Destroy(bullet.gameObject);

                animator.Play("Damage");
            }

            text.text = health.ToString();
        }

        if (!other.CompareTag("Player")) return;

        var enemyTag = gameObject.tag;

        if (enemyTag == "Enemy")
        {
            GameManager.Instance.GameOver();
        }
        else if (enemyTag == "PodiumEnemy")
        {
            GameManager.Instance.LevelCompleted();
        }
    }

    private IEnumerator DeleteEnemey()
    {
        var skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        var materials = skinnedMeshRenderer.materials;
        
        float currentTime = 0;
        while (currentTime < GameManager.Instance.enemyDeleteTime)
        {
            float t = currentTime / GameManager.Instance.enemyDeleteTime;
            materials[0].color = GameManager.Instance.enemyDeadGradient.Evaluate(t);
            currentTime += Time.deltaTime;
            
            yield return new WaitForEndOfFrame();

            skinnedMeshRenderer.materials = materials;
        }

        particleDestroy.transform.parent = null;
        particleDestroy.Play();
        
        money.transform.parent = null;
        money.GetComponent<BoxCollider>().isTrigger = false;
        money.GetComponent<Rigidbody>().useGravity = true;
        money.transform.eulerAngles = Vector3.zero;
        money.SetActive(true);
        
        Destroy(gameObject);
    }
}