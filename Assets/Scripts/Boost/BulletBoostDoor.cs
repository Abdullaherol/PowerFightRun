using System;
using UnityEngine;

public class BulletBoostDoor : MonoBehaviour, IBoostDoorType
{
    [SerializeField] private float value;
    private BoostManager _boostManager;

    private void Start()
    {
        _boostManager = BoostManager.Instance;
    }

    public void ApplyEffect()
    {
        _boostManager.boostValue.bullet += (int)value;
    }

    public DoorBoostType GetBoostType()
    {
        return DoorBoostType.Bullet;
    }
}