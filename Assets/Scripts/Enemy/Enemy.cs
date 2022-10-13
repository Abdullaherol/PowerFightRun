using System;
using System.Collections;
using Unity.VisualScripting;
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
        var enemyTag = gameObject.tag;
        
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

            if (enemyTag == "PodiumEnemy")
            {
                GameManager.Instance.onPodium = true;
            } 
        }

        if (!other.CompareTag("Player")) return;

        if (enemyTag == "Enemy")
        {
            GameManager.Instance.GameOver();
        }
        else if (enemyTag == "PodiumEnemy")
        {
            var podiumParticle = GameObject.FindGameObjectWithTag("PodiumParticle");

            podiumParticle.transform.position = new Vector3(podiumParticle.transform.position.x, podiumParticle.transform.position.y, other.transform.position.z);

            foreach (var componentsInChild in podiumParticle.GetComponentsInChildren<ParticleSystem>())
            {
                componentsInChild.Play();
            }
        
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
        money.GetComponent<Rigidbody>().useGravity = true;
        money.SetActive(true);
        
        Destroy(gameObject);
    }
}