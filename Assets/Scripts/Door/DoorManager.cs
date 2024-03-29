﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DoorManager : MonoBehaviour
{
    public static DoorManager Instance;
    public List<DoorParent> doors = new List<DoorParent>();
    public DoorSetting doorSetting;

    private void Awake()
    {
        Instance = this;
    }

    public string GetTypeText(DoorType type)
    {
        return doorSetting.typeSettings.FirstOrDefault(x => x.type == type).text;
    }
    
    public string GetTitle(DoorType type)
    {
        return doorSetting.typeSettings.FirstOrDefault(x => x.type == type).title;
    }

    public Material GetMaterial(DoorType type)
    {
        return (type == DoorType.ChangeWeapon || type == DoorType.IncreaseBullet || type == DoorType.IncreasePlayer ||
                type == DoorType.IncreaseFireRate || type == DoorType.IncreaseDistance)
            ? doorSetting.increaseMaterial
            : doorSetting.decreaseMaterial;
    }

    public Color GetBodyColor(DoorType type)
    {
        return (type == DoorType.ChangeWeapon || type == DoorType.IncreaseBullet || type == DoorType.IncreasePlayer ||
                type == DoorType.IncreaseFireRate || type == DoorType.IncreaseDistance)
            ? doorSetting.increaseBodyColor
            : doorSetting.DecreaseBodyColor;
    }
    
    public bool IsPositiveDoor(DoorType type)
    {
        return (type == DoorType.ChangeWeapon || type == DoorType.IncreaseBullet || type == DoorType.IncreasePlayer ||
                type == DoorType.IncreaseFireRate || type == DoorType.IncreaseDistance)
            ? true
            : false;
    }

    public bool IsImageDoor(DoorType type)
    {
        return doorSetting.typeSettings.FirstOrDefault(x => x.type == type).imageDoor;
    }

    public Sprite GetImageDoorSprite(DoorType type)
    {
        return doorSetting.typeSettings.FirstOrDefault(x => x.type == type).image;
    }

    public float GetMultiplier(DoorType type)
    {
        return doorSetting.typeSettings.FirstOrDefault(x => x.type == type).multiplier;
    }
}