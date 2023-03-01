using UnityEngine;

public class PlayerDoor : MonoBehaviour, IDoorType
{
    [SerializeField] private bool _increase;

    private PlayerManager _playerManager;
    private DoorManager _doorManager;

    private void Start()
    {
        _playerManager = PlayerManager.Instance;
        _doorManager = DoorManager.Instance;
    }

    public void ApplyEffect()
    {
        if (_increase)
        {
            _playerManager.IncreasePlayer();
        }
        else
        {
            _playerManager.DecreasePlayer();
        }
    }
    
    public bool IsPositiveDoor()
    {
        return _increase;
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
        return (_increase) ? DoorType.IncreasePlayer : DoorType.DecreasePlayer;
    }
}