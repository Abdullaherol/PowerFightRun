using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public GameObject panel;
    
    public void ReTry()
    {
        GameManager.Instance.ReTry();
    }

    public void ShowUI()
    {
        panel.SetActive(true);
    }
}