using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ThrowManager : MonoBehaviour
{
    public static ThrowManager Instance;

    [Space, Header("Test Data")] public int playerCount;
    public int bulletCount;
    
    [Space]
    public ThrowWeapon currentWeapon;
    public ThrowUpgrade upgrade;

    [Space, Header("Bullet")] public ParticleSystem bulletCollisionParticle;

    private float _currentTime;
    private bool _shot;

    private PlayerManager _playerManager;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        RefreshUpgrade();
        
        _playerManager = PlayerManager.Instance;
    }

    private void Update()
    {
        if(!_shot || GameManager.Instance.failed || !GameManager.Instance.gameStarted) return;
        
        if(currentWeapon==null) return;

        var boostValue = BoostManager.Instance.boostValue;

        if (_currentTime < upgrade.fireRate + boostValue.fireRate)
        {
            _currentTime += Time.deltaTime;
        }
        else
        {
            if (upgrade.bulletCount + boostValue.bullet >= currentWeapon.settings.Count)
                upgrade.bulletCount = currentWeapon.settings.Count;
            else if(upgrade.bulletCount < 1)
                upgrade.bulletCount = 1;
            
            
            foreach (var playerPosition in _playerManager.playerPositions[_playerManager.playerCount-1].playerPositions)
            {
                var position = _playerManager.moveParent.transform.position + playerPosition;
            
                foreach (var weaponDirection in currentWeapon.settings[upgrade.bulletCount-1].directions)
                {
                    var spawnPoint = position + weaponDirection.position;
                    var bullet = Instantiate(currentWeapon.bullet);
                    bullet.transform.position = spawnPoint;
                    bullet.transform.LookAt(spawnPoint + weaponDirection.direction + new Vector3(Random.Range(-currentWeapon.spread,currentWeapon.spread),0,0));
                }
            }
            _currentTime = 0;
        }
    }

    public void Shot(bool shot)
    {
        _shot = shot;
        
        if (!shot)
            _currentTime = 0;
    }

    private void OnDrawGizmos()
    {
        try
        {
            PlayerManager playerManager = GameObject.FindObjectOfType<PlayerManager>();
            
            foreach (var playerPosition in playerManager.playerPositions[playerCount-1].playerPositions)
            {
                var position = playerManager.moveParent.transform.position + playerPosition;
                foreach (var weaponDirection in currentWeapon.settings[bulletCount-1].directions)
                {
                    var spawnPoint = position + weaponDirection.position;
                
                    Debug.DrawLine(playerManager.moveParent.transform.position,position,Color.green);
                    Debug.DrawLine(spawnPoint,spawnPoint + weaponDirection.direction,Color.red);
                }
            }
        }
        catch (ArgumentOutOfRangeException e)
        {
        }
    }

    public void ChangeWeapon(CollectableWeapon weapon)
    {
        currentWeapon = weapon.weapon;

        _currentTime = weapon.weapon.fireRate;

        RefreshUpgrade();
        
        _playerManager.UpdatePlayersWeapons(weapon.weapon);
    }
    
    public void ChangeWeapon(ThrowWeapon weapon)
    {
        currentWeapon = weapon;

        RefreshUpgrade();
        
        _playerManager.UpdatePlayersWeapons(weapon);
    }

    private void RefreshUpgrade()
    {
        upgrade.fireRate = currentWeapon.fireRate;
        upgrade.time = currentWeapon.bullet.GetComponent<ThrowBullet>().time;
    }
}
