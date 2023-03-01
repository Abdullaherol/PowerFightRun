using System;
using UnityEngine;

public class LevelStartUI : MonoBehaviour
{
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    public void StartGame()
    {
        _gameManager.StartGame();
    }
}