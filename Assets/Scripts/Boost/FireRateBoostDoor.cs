using UnityEngine;

public class FireRateBoostDoor : MonoBehaviour,IBoostDoorType
{
    [SerializeField] private float value;
    private BoostManager _boostManager;

    private void Start()
    {
        _boostManager = BoostManager.Instance;
    }

    public void ApplyEffect()
    {
        _boostManager.boostValue.fireRate = value;
    }

    public DoorBoostType GetBoostType()
    {
        return DoorBoostType.FireRate;
    }
}