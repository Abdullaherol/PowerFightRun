using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DoorSettings", order = 0)]
public class DoorSetting : ScriptableObject
{
    public Material increaseMaterial;
    public Material decreaseMaterial;
    public Color increaseBodyColor;
    public Color DecreaseBodyColor;
    public List<DoorTypeSetting> typeSettings = new List<DoorTypeSetting>();
}