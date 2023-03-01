using UnityEngine;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private TMPro.TextMeshProUGUI _soundText;
    [SerializeField] private TMPro.TextMeshProUGUI _vibrationText;
    [SerializeField] private bool _sound;
    [SerializeField] private bool _vibration;

    private void Start()
    {
        _sound = (PlayerPrefs.GetInt("Sound", 1) == 1) ? true : false;
        _vibration = (PlayerPrefs.GetInt("Vibration", 1) == 1) ? true : false;
    }

    public void ChangeSound()
    {
        _sound = !_sound;
        _soundText.text = (_sound) ? "On" : "Off";
    }

    public void ChangeVibration()
    {
        _vibration = !_vibration;
        _vibrationText.text = (_vibration) ? "On" : "Off";
    }

    public void ShowSettings()
    {
        _panel.SetActive(true);
    }

    public void HideSettings()
    {
        _panel.SetActive(false);
    }
}