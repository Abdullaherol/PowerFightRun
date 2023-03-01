using UnityEngine;

public class Player : MonoBehaviour,IEntity
{
    public ParticleSystem particleDoor;
    public Animator animatorCharacter;
    
    [SerializeField] private Transform _weaponParent;
    [SerializeField] private GameObject _currentWeapon;
    [SerializeField] private ThrowWeapon _weapon;
    [SerializeField] private Animator _animatorParent;

    public void ChangeWeapon(ThrowWeapon throwWeapon)
    {
        if (_currentWeapon != null)
        {
            if (_weapon != throwWeapon)
                Destroy(_currentWeapon);
            else return;
        }
        
        _animatorParent.Play("ChangeWeapon");

        var weaponTransform = Instantiate(throwWeapon.prefab).transform;

        weaponTransform.parent = _weaponParent;
        weaponTransform.localPosition = throwWeapon.position;
        weaponTransform.localEulerAngles = throwWeapon.rotation;
        weaponTransform.localScale = throwWeapon.scale;

        _currentWeapon = weaponTransform.gameObject;
        _weapon = throwWeapon;
    }

    public EntityType GetEntityType()
    {
        return EntityType.Player;
    }
}