using UnityEngine;

[RequireComponent(typeof(TMPro.TextMeshProUGUI))]
public class LevelText : MonoBehaviour
{
    private TMPro.TextMeshProUGUI _text;
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;

        _text = GetComponent<TMPro.TextMeshProUGUI>();
        _text.text = "Level " + _gameManager.fakeLevel.ToString();
    }
}