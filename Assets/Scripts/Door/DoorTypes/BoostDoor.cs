using System;
using UnityEngine;

public class BoostDoor : MonoBehaviour, IDoorType
{
    [SerializeField] private IBoostDoorType _type;
    [SerializeField] private float _value;

    private DoorManager _doorManager;

    private void Start()
    {
        _doorManager = DoorManager.Instance;
    }

    public void ApplyEffect()
    {
        _type.ApplyEffect();
    }
    
    public bool IsPositiveDoor()
    {
        return _value >= 0;
    }
    
    public string GetTypeText()
    {
        return _doorManager.GetTypeText(GetDoorType());
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
        return DoorType.Boost;
    }
}