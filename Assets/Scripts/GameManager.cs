using System;
using System.Collections.Generic;
using System.Linq;
using Lofelt.NiceVibrations;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool failed;
    public bool gameStarted;
    public int money;
    public int level;
    public int fakeLevel;
    public bool onPodium;

    [Space, Header("Enemy Setting")] public float enemyDeleteTime;
    public Gradient enemyDeadGradient;

    [Space, Header("Money Settings")] public int eachMoneyValue;

    [Space, Header("Collectable Settings")]
    public ParticleSystem collectableGroundParticle;

    private GameOverUI _gameOverUI;
    private MoneyText _moneyText;
    private LevelCompletedUI _levelCompletedUI;

    private List<ThrowWeapon> _weapons = new List<ThrowWeapon>();

    private float _currentTimeTotal;
    private float _currentTimePodium;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        Instance = this;

        GetSettings();
    }

    private void GetWeaponsFromAssets()
    {
        _weapons = Resources.LoadAll<ThrowWeapon>("Weapons").ToList();
    }

    public ThrowWeapon GetRandomWeapon(ThrowWeapon currentWeapon)
    {
        List<ThrowWeapon> weapons = new List<ThrowWeapon>(_weapons);
        weapons.Remove(currentWeapon);
        return weapons[Random.Range(0, weapons.Count)];
    }

    private void Start()
    {
        _gameOverUI = GameObject.FindObjectOfType<GameOverUI>();
        _moneyText = GameObject.FindObjectOfType<MoneyText>();
        _levelCompletedUI = GameObject.FindObjectOfType<LevelCompletedUI>();
    }

    public void GameOver()
    {
        failed = true;
        _gameOverUI.ShowUI();
    }

    public void ReTry()
    {
        LoadScene();
    }

    public void LevelCompleted()
    {
        gameStarted = false;

        _levelCompletedUI.ShowUI();
    }

    public void NextLevel()
    {
        int count = SceneManager.sceneCountInBuildSettings;
        level++;
        fakeLevel++;

        if (level >= count)
        {
            level = 0;
        }

        SaveSettings();

        LoadScene();
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetInt("FakeLevel", fakeLevel);
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.SetInt("Level", level);
    }

    private void GetSettings()
    {
        money = PlayerPrefs.GetInt("Money", 0);
        level = PlayerPrefs.GetInt("Level", 0);
        fakeLevel = PlayerPrefs.GetInt("FakeLevel", 1);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(level);
    }

    public void AddMoney(int amount)
    {
        money += amount;

        PlayerPrefs.SetInt("Money", money);
        
        GameManager.Instance.Vibrate();

        _moneyText.UpdateText();
    }

    public void StartGame()
    {
        gameStarted = true;
    }

    private void Update()
    {
        if (!gameStarted) return;

        _currentTimeTotal += Time.deltaTime;

        if (onPodium)
            _currentTimePodium += Time.deltaTime;

        Debug.Log("Total: " + _currentTimeTotal.ToString("F") + "  Podium: " + _currentTimePodium.ToString("F"));
    }

    public void Vibrate()
    {
        HapticPatterns.PlayConstant(.15f, 0.0f, .05f);
    }
}