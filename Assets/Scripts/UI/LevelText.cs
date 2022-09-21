using System;
using UnityEngine;

[RequireComponent(typeof(TMPro.TextMeshProUGUI))]
public class LevelText : MonoBehaviour
{
    private TMPro.TextMeshProUGUI _text;

    private void Start()
    {
        _text = GetComponent<TMPro.TextMeshProUGUI>();
        _text.text = "Level "+GameManager.Instance.fakeLevel.ToString();
    }
}