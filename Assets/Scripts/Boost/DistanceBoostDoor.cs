using UnityEngine;

public class DistanceBoostDoor : MonoBehaviour,IBoostDoorType
{
    [SerializeField] private float value;
    private BoostManager _boostManager;

    private void Start()
    {
        _boostManager = BoostManager.Instance;
    }

    public void ApplyEffect()
    {
        _boostManager.boostValue.distance = value;
    }

    public DoorBoostType GetBoostType()
    {
        return DoorBoostType.Distance;
    }
}