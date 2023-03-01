using UnityEngine;

[RequireComponent(typeof(TMPro.TextMeshProUGUI))]
public class MoneyText : MonoBehaviour
{
    private TMPro.TextMeshProUGUI text;
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        
        text = GetComponent<TMPro.TextMeshProUGUI>();
        UpdateText();
    }
    public void UpdateText()
    {
        text.text = _gameManager.money.ToString();
    }
}