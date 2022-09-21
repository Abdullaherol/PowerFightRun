using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool failed;
    public bool gameStarted;
    public int money;
    public int level;
    public int fakeLevel;

    [Space, Header("Enemy Setting")] public float enemyDeleteTime;
    public Gradient enemyDeadGradient;

    [Space, Header("Money Settings")] public int eachMoneyValue;
    
    private GameOverUI _gameOverUI;
    private MoneyText _moneyText;
    private LevelCompletedUI _levelCompletedUI;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        
        Instance = this;
        
        GetSettings();
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
        PlayerPrefs.SetInt("FakeLevel",fakeLevel);
        PlayerPrefs.SetInt("Money",money);
        PlayerPrefs.SetInt("Level",level);
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
        
        PlayerPrefs.SetInt("Money",money);
        
        _moneyText.UpdateText();
    }

    public void StartGame()
    {
        gameStarted = true;
    }
}