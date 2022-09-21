using UnityEngine;

public class LevelCompletedUI : MonoBehaviour
{
    public GameObject panel;

    public void ShowUI()
    {
        panel.SetActive(true);
    }

    public void NextLevel()
    {
        GameManager.Instance.NextLevel();
    }
}