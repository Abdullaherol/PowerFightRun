﻿#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorParent : MonoBehaviour
{
    [Header("Left")] public DoorChild leftDoor;
    public DoorType leftType;
    public float leftValue;
    [Space, Header("Right")] public DoorChild rightDoor;
    public DoorType rightType;
    public float rightValue;

    private bool used;

    private void Start()
    {
        if (leftDoor != null)
            leftDoor.SetParent(this);

        if (rightDoor != null)
            rightDoor.SetParent(this);

        RefreshDoor(DoorManager.Instance);
    }

    public void RefreshDoor(DoorManager doorManager)
    {
        RefreshChild(leftDoor,leftType,leftValue,doorManager);
        RefreshChild(rightDoor,rightType,rightValue,doorManager);
    }

    private void RefreshChild(DoorChild child,DoorType type,float value,DoorManager doorManager)
    {
        if(child == null || doorManager == null) return;
        
        var doorImage = doorManager.IsImageDoor(type);
        var multiplier = doorManager.GetMultiplier(type);
            
       child.title.text = doorManager.GetTitle(type);
       child.text.text = (doorImage) ? "" : string.Format(doorManager.GetTypeText(type), value * multiplier);
       child.meshRenderer.material = doorManager.GetMaterial(type);
       child.bodyImage.color = (doorImage) ? Color.clear : doorManager.GetBodyColor(type);
       child.image.sprite = (doorImage) ? doorManager.GetImageDoorSprite(type) : null;
       child.image.color = (doorImage) ? Color.white : Color.clear;
    }

    public void Trigger(DoorChild child)
    {
        if (used) return;

        DoorChild door = leftDoor;
        bool left = true;

        if (leftDoor != null)
        {
            if (leftDoor == child)
            {
                door = child;
                left = true;
            }
        }

        if (rightDoor != null)
        {
            if (rightDoor == child)
            {
                door = child;
                left = false;
            }
        }

        if (door == null) return;

        var type = (left) ? leftType : rightType;
        var value = (left) ? leftValue : rightValue;

        if (type == DoorType.IncreaseFireRate)
        {
            ThrowManager.Instance.upgrade.fireRate -= value;
        }
        else if (type == DoorType.DecreaseFireRate)
        {
            ThrowManager.Instance.upgrade.fireRate += value;
        }
        else if (type == DoorType.IncreaseBullet)
        {
            ThrowManager.Instance.upgrade.bulletCount += (int)value;
        }
        else if (type == DoorType.ChangeWeapon)
        {
        }
        else if (type == DoorType.RandomWeapon)
        {
        }
        else if (type == DoorType.IncreasePlayer)
        {
            PlayerManager.Instance.IncreasePlayer();
        }
        else if (type == DoorType.DecreasePlayer)
        {
            PlayerManager.Instance.DecreasePlayer();
        }

        DeActivateDoors();
    }

    private void DeActivateDoors()
    {
        used = true;

        gameObject.SetActive(false);
    }

    public int GetMaxPlayerCount()
    {
        bool hasDoor = false;
        if (leftDoor != null)
        {
            if (leftType == DoorType.IncreasePlayer)
                hasDoor = true;
        }

        if (rightDoor != null)
        {
            if (rightType == DoorType.IncreasePlayer)
                hasDoor = true;
        }

        return (hasDoor) ? 1 : 0;
    }
}