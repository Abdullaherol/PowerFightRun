using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Weapon", order = 1)]
public class ThrowWeapon : ScriptableObject
{
    public GameObject prefab;
    public GameObject bullet;
    public List<ThrowWeaponSetting> settings = new List<ThrowWeaponSetting>();
    public float fireRate;
    public float spread;
    
    [Space,Header("Player Back Setting")]
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;
}