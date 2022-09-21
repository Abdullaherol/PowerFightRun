using UnityEngine;

[System.Serializable]
public class ThrowUpgrade
{
     [Header("Weapon")]
     public float fireRate;
     public int bulletCount;
     [Space, Header("Bullet")] public float time;
}