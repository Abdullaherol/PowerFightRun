using System;
using System.Linq;
using UnityEngine;

public class BoostManager : MonoBehaviour
{
    public static BoostManager Instance;

    public BoostSetting setting;
    public BoostValue boostValue;
    public DoorBoostType currentBoost;
    public float time;
    private float _currentTime;

    private void Awake()
    {
        Instance = this;
        boostValue = new BoostValue();
    }

    private void Update()
    {
        if (_currentTime < time)
        {
            _currentTime += Time.deltaTime;
        }
        else
        {
            boostValue = new BoostValue();
        }
    }

    public void Boost(IBoostDoorType type,float value)
    {
        currentBoost = type.GetBoostType();
        _currentTime = 0;
        time = setting.boosts.First(x => x.type == currentBoost).time;
        
        type.ApplyEffect();
    }
}