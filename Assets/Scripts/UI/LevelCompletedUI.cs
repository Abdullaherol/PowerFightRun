using System;
using UnityEngine;

public class LevelCompletedUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    private GameManager _gameManager;
    
    private void Start()
    {
        _gameManager = GameManager.Instance;

        _gameManager.OnLevelCompleted += OnLevelCompleted;
    }

    private void OnLevelCompleted()
    {
        ShowUI();
    }

    private void ShowUI()
    {
        panel.SetActive(true);
    }

    public void NextLevel()
    {
        _gameManager.NextLevel();
    }
}