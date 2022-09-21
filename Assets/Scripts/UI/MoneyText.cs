using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(TMPro.TextMeshProUGUI))]
public class MoneyText : MonoBehaviour
{
    private TMPro.TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
        UpdateText();
    }
    public void UpdateText()
    {
        text.text = GameManager.Instance.money.ToString();
    }
}