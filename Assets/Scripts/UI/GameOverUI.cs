using System;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public GameObject panel;

    private void Start()
    {
        GameManager.Instance.OnFailed += ShowUI;
    }

    public void ReTry()
    {
        GameManager.Instance.ReTry();
    }

    private void ShowUI()
    {
        panel.SetActive(true);
    }
}