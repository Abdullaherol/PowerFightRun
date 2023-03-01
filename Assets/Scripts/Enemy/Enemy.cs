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

    private GameManager _gameManager;

    void Start()
    {
        _gameManager = GameManager.Instance;
        
        text.text = health.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemyTag = GetComponent<IEntity>().GetEntityType();

        var triggerTag = other.GetComponent<IEntity>().GetEntityType();
        
        if (triggerTag == EntityType.Bullet)
        {
            var bullet = other.GetComponent<ThrowBullet>();

            if (health - bullet.damage <= 0)
            {
                animator.Play("Dead");

                particleDead.transform.parent = null;
                particleDead.Play();
                
                enabled = false;

                StartCoroutine(DeleteEnemy());
            }
            else
            {
                health -= bullet.damage;
                Destroy(bullet.gameObject);

                animator.Play("Damage");
            }

            text.text = health.ToString();

            if (enemyTag == EntityType.PodiumEnemy)
            {
                _gameManager.onPodium = true;
            } 
        }

        if (triggerTag != EntityType.Player) return;

        if (enemyTag == EntityType.Enemy)
        {
            _gameManager.GameOver();
        }
        else if (enemyTag == EntityType.PodiumEnemy)
        {
            var podiumParticle = GameObject.FindGameObjectWithTag("PodiumParticle");

            podiumParticle.transform.position = new Vector3(podiumParticle.transform.position.x, podiumParticle.transform.position.y, other.transform.position.z);

            foreach (var componentsInChild in podiumParticle.GetComponentsInChildren<ParticleSystem>())
            {
                componentsInChild.Play();
            }
        
            _gameManager.LevelCompleted();
        }
    }

    private IEnumerator DeleteEnemy()
    {
        var skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        var materials = skinnedMeshRenderer.materials;
        
        float currentTime = 0;
        while (currentTime < _gameManager.enemyDeleteTime)
        {
            var t = currentTime / _gameManager.enemyDeleteTime;
            materials[0].color = _gameManager.enemyDeadGradient.Evaluate(t);
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