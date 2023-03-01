using UnityEngine;

public class WeaponDoor : MonoBehaviour,IDoorType
{
    [SerializeField] private bool _random;
    [SerializeField] private ThrowWeapon _weapon;

    private ThrowManager _throwManager;
    private GameManager _gameManager;
    private DoorManager _doorManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _throwManager = ThrowManager.Instance;
        _doorManager = DoorManager.Instance;
    }

    public void ApplyEffect()
    {
        var currentWeapon = _throwManager.currentWeapon;

        var weapon = (_random) ? _gameManager.GetRandomWeapon(currentWeapon) : _weapon;
        
        _throwManager.ChangeWeapon(weapon);
    }  
    
    public bool IsPositiveDoor()
    {
        return true;
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
        return (_random) ? DoorType.RandomWeapon : DoorType.ChangeWeapon;
    }
}