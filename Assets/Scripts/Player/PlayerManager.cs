using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    
    public int playerCount;
    public GameObject prefab;
    public Transform moveParent;
    public int moveTime;
    public List<PlayerPosition> playerPositions = new List<PlayerPosition>();

    private List<GameObject> players = new List<GameObject>();
    private List<GameObject> playerPool = new List<GameObject>();

    private float _currentTime;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        var count = /*DoorManager.Instance.GetMaxPlayerCount()*/  + 10;

        if (playerPositions.Count != count)
            Debug.LogWarning("PlayerManager: Maximum oyuncu sayısı ile oyuncu pozisyon sayıları uyuşmuyor.");

        for (int i = 0; i < count; i++)
        {
            var player = Instantiate(prefab);
            player.SetActive(false);
            player.transform.position = new Vector3(100, 100, 100);

            playerPool.Add(player);
        }

        IncreasePlayer(true);
    }

    public void IncreasePlayer(bool playParticle)
    {
        var player = playerPool[^1];

        playerPool.RemoveAt(playerPool.Count - 1);

        player.SetActive(true);
        player.transform.parent = moveParent;
        player.transform.localPosition = (players.Count > 0) ? players[^1].transform.localPosition : playerPositions[0].playerPositions[0];

        players.Add(player);
        
        player.GetComponent<Player>().ChangeWeapon(ThrowManager.Instance.currentWeapon,playParticle);

        _currentTime = 0;

        playerCount++;
    }

    public void PlayRunAnim()
    {
        PlayAnim("Run");
    }

    public void PlayIdleAnim()
    {
        PlayAnim("Idle");
    }

    private void PlayAnim(string anim)
    {
        for (int i = 0; i < players.Count; i++)
        {
            var player = players[i];
            var animator = player.GetComponent<Player>().animatorCharacter;
            animator.Play(anim);
        }
    }

    public void DecreasePlayer()
    {
        if (players.Count > 0)
        {
            var player = players[^1];
            
            players.Remove(player);
            
            playerPool.Add(player);
            player.SetActive(false);
            player.transform.parent = null;
            player.transform.position = new Vector3(100, 100, 100);

            playerCount--;

            if (players.Count == 0)
            {
                GameManager.Instance.GameOver();
            }
        }
    }

    private void Update()
    {
        if (_currentTime < moveTime)
        {
            for (int i = 0; i < players.Count; i++)
            {
                var player = players[i];
                var position = playerPositions[playerCount-1].playerPositions[i];

                float t = _currentTime / moveTime;

                player.transform.localPosition = Vector3.Lerp(player.transform.localPosition, position, t);
            }

            _currentTime += Time.deltaTime;
        }
    }
    
    public void UpdatePlayersWeapons(ThrowWeapon weapon)
    {
        for (int i = 0; i < players.Count; i++)
        {
            var p = players[i];
            var player = p.GetComponent<Player>();
            
            player.ChangeWeapon(weapon,true);
        }
    }
}