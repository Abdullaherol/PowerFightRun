using System;
using UnityEngine;

public class FireRateDoor : MonoBehaviour,IDoorType
{
    [SerializeField] private float _value;

    private ThrowManager _throwManager;
    private DoorManager _doorManager;

    private void Start()
    {
        _doorManager = DoorManager.Instance;
        _throwManager = ThrowManager.Instance;
    }

    public void ApplyEffect()
    {
        _throwManager.upgrade.fireRate += _value;
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
        return (_value >= 0) ? DoorType.IncreaseFireRate : DoorType.DecreaseFireRate;
    }
}