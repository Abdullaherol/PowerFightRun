using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Boost", menuName = "ScriptableObjects/BoostSetting", order = 0)]
public class BoostSetting : ScriptableObject
{
    public List<BoostTypeSetting> settings = new List<BoostTypeSetting>();
}