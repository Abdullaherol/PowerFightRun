using UnityEngine;

public class LevelStartUI : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }
}