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
    [SerializeField] private int bulletCount;

    [Space] public ThrowWeapon currentWeapon;
    public ThrowUpgrade upgrade;

    [Space, Header("Bullet")] public ParticleSystem bulletCollisionParticle;

    private float _currentTime;
    private bool _shot;
    private bool _failed;
    private bool _gameStarted;

    private BoostManager _boostManager;
    private PlayerManager _playerManager;
    private GameManager _gameManager;

    private Dictionary<ThrowWeapon, GameObject> _bulletPool = new Dictionary<ThrowWeapon, GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        RefreshUpgrade();

        _gameManager = GameManager.Instance;
        _playerManager = PlayerManager.Instance;
        _boostManager = BoostManager.Instance;

        _gameManager.OnFailed += OnFailed;
        _gameManager.OnGameStarted += OnGameStarted;
    }

    private void OnFailed()
    {
        _failed = true;
    }

    private void OnGameStarted()
    {
        _gameStarted = true;
    }

    private void Update()
    {
        if (!_shot || _failed || !_gameStarted) return;

        if (currentWeapon == null) return;

        var boostValue = _boostManager.boostValue;

        if (_currentTime < upgrade.fireRate + boostValue.fireRate)
        {
            _currentTime += Time.deltaTime;
        }
        else
        {
            if (upgrade.bulletCount + boostValue.bullet >= currentWeapon.settings.Count)
            {
                upgrade.bulletCount = currentWeapon.settings.Count;
            }
            else if (upgrade.bulletCount < 1)
            {
                upgrade.bulletCount = 1;
            }


            foreach (var playerPosition in _playerManager.playerPositions[_playerManager.playerCount - 1]
                         .playerPositions)
            {
                var position = _playerManager.moveParent.transform.position + playerPosition;

                foreach (var weaponDirection in currentWeapon.settings[upgrade.bulletCount - 1].directions)
                {
                    var spawnPoint = position + weaponDirection.position;
                    var bullet = GetBullet();
                    bullet.transform.position = spawnPoint;
                    bullet.GetComponent<ThrowBullet>().direction = weaponDirection.direction
                                                                   + new Vector3(
                                                                       Random.Range(-currentWeapon.spread,
                                                                           currentWeapon.spread), 0, 0);
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
            var playerManager = FindObjectOfType<PlayerManager>();

            foreach (var playerPosition in playerManager.playerPositions[playerCount - 1].playerPositions)
            {
                var position = playerManager.moveParent.transform.position + playerPosition;
                foreach (var weaponDirection in currentWeapon.settings[bulletCount - 1].directions)
                {
                    var spawnPoint = position + weaponDirection.position;

                    Debug.DrawLine(playerManager.moveParent.transform.position, position, Color.green);
                    Debug.DrawLine(spawnPoint, spawnPoint + weaponDirection.direction, Color.red);
                }
            }
        }
        catch (ArgumentOutOfRangeException e)
        {
            Debug.Log("Editor error on drawing gizmos");
        }
    }

    public void ChangeWeapon(CollectableWeapon weapon)
    {
        currentWeapon = weapon.weapon;

        _currentTime = weapon.weapon.fireRate;

        _gameManager.Vibrate();

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

    public GameObject GetBullet()
    {
        if (_bulletPool.ContainsKey(currentWeapon))
        {
            var bullet = _bulletPool[currentWeapon];
            _bulletPool.Remove(currentWeapon);

            var throwB = bullet.GetComponent<ThrowBullet>();
            throwB.weapon = currentWeapon;
            throwB.ReShot();

            return bullet;
        }
        else
        {
            var bulletInstantiated = Instantiate(currentWeapon.bullet);
            var throwBullet = bulletInstantiated.GetComponent<ThrowBullet>();
            throwBullet.weapon = currentWeapon;

            return bulletInstantiated;
        }
    }

    public void AddBullet(GameObject bullet, ThrowWeapon weapon)
    {
        bullet.SetActive(false);

        _bulletPool.Add(weapon, bullet);
    }
}