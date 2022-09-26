using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    public GameObject panel;
    public Button soundBtn;
    public Button vibrationBtn;
    public TMPro.TextMeshProUGUI soundText;
    public TMPro.TextMeshProUGUI vibrationText;
    public bool sound;
    public bool vibration;

    private void Start()
    {
        sound = (PlayerPrefs.GetInt("Sound", 1) == 1) ? true : false;
        vibration = (PlayerPrefs.GetInt("Vibration", 1) == 1) ? true : false;
    }

    public void ChangeSound()
    {
        sound = !sound;
        soundText.text = (sound) ? "On" : "Off";
    }

    public void ChangeVibration()
    {
        vibration = !vibration;
        vibrationText.text = (vibration) ? "On" : "Off";
    }

    public void ShowSettings()
    {
        panel.SetActive(true);
    }

    public void HideSettings()
    {
        panel.SetActive(false);
    }
}