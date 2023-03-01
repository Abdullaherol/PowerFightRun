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
    public int money;
    public int level;
    public int fakeLevel;
    public bool onPodium;
    
    //Handlers
    public delegate void OnFailedHandler();
    public delegate void OnGameStartedHandler();
    public delegate void OnLevelCompletedHandler();
    //Handlers
    
    //Events
    public event OnFailedHandler OnFailed;
    public event OnGameStartedHandler OnGameStarted;
    public event OnLevelCompletedHandler OnLevelCompleted;
    //Events
    

    [Space, Header("Enemy Setting")] public float enemyDeleteTime;
    public Gradient enemyDeadGradient;

    [Space, Header("Money Settings")] public int eachMoneyValue;

    [Space, Header("Collectable Settings")]
    public ParticleSystem collectableGroundParticle;

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

    public ThrowWeapon GetRandomWeapon(ThrowWeapon currentWeapon)
    {
        List<ThrowWeapon> weapons = new List<ThrowWeapon>(_weapons);
        weapons.Remove(currentWeapon);
        return weapons[Random.Range(0, weapons.Count)];
    }

    private void Start()
    {
        _moneyText = GameObject.FindObjectOfType<MoneyText>();
        _levelCompletedUI = GameObject.FindObjectOfType<LevelCompletedUI>();
    }

    public void GameOver()
    {
        OnFailed?.Invoke();
    }

    public void ReTry()
    {
        LoadScene();
    }

    public void LevelCompleted()
    {
        OnLevelCompleted?.Invoke();
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
        
        Vibrate();

        _moneyText.UpdateText();
    }

    public void StartGame()
    {
        OnGameStarted?.Invoke();
    }

    public void Vibrate()
    {
        HapticPatterns.PlayConstant(.15f, 0.0f, .05f);
    }
}