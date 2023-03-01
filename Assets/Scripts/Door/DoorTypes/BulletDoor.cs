using System;
using UnityEngine;

public class BulletDoor : MonoBehaviour,IDoorType
{
    [SerializeField] private int _value;

    private ThrowManager _throwManager;
    private DoorManager _doorManager;

    private void Start()
    {
        _throwManager = ThrowManager.Instance;
        _doorManager = DoorManager.Instance;
    }

    public void ApplyEffect()
    {
        _throwManager.upgrade.bulletCount += _value;
    }

    public bool IsPositiveDoor()
    {
        return _value >= 0;
    }
    
    public string GetTypeText()
    {
        return String.Format(_doorManager.GetTypeText(GetDoorType()), _value * GetMultiplier());
    }

    public string GetTitle()
    {
        return _doorManager.GetTitle(GetDoorType());
    }

    public Material GetMaterial()
    {
        return _doorManager.GetMaterial(GetDoorType());
    }

    public Color GetBodyColor()
    {
        return _doorManager.GetBodyColor(GetDoorType());
    }

    public bool IsImageDoor()
    {
        return _doorManager.IsImageDoor(GetDoorType());
    }

    public Sprite GetImageDoorSprite()
    {
        return _doorManager.GetImageDoorSprite(GetDoorType());
    }

    public float GetMultiplier()
    {
        return _doorManager.GetMultiplier(GetDoorType());
    }

    public DoorType GetDoorType()
    {
        return (_value >= 0) ? DoorType.IncreaseBullet : DoorType.DecreaseBullet;
    }
}