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

    public void Boost(DoorBoostType type,float value)
    {
        currentBoost = type;
        _currentTime = 0;
        time = setting.settings.FirstOrDefault(x => x.type == type).time;
        
        if (currentBoost == DoorBoostType.Bullet)
        {
            boostValue.bullet += (int)value;
        }
        else if (currentBoost == DoorBoostType.Distance)
        {
            boostValue.distance = value;
        }
        else if (currentBoost == DoorBoostType.FireRate)
        {
            boostValue.fireRate = value;
        }
        else if (currentBoost == DoorBoostType.AutoCollectMoney)
        {
        }
    }
}