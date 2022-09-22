using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform weaponParent;
    public GameObject currentWeapon;
    public ThrowWeapon weapon;
    public ParticleSystem particleChangeWeapon;
    public Animator animator;
    
    public void ChangeWeapon(ThrowWeapon throwWeapon)
    {
        if (currentWeapon != null)
        {
            if(weapon != throwWeapon)
                Destroy(currentWeapon);
            else return;
        }
        
        particleChangeWeapon.Play();
        animator.Play("ChangeWeapon");

        var weaponTransform = Instantiate(throwWeapon.prefab).transform;
        
        weaponTransform.parent = weaponParent;
        weaponTransform.localPosition = throwWeapon.position;
        weaponTransform.localEulerAngles = throwWeapon.rotation;
        weaponTransform.localScale = throwWeapon.scale;

        currentWeapon = weaponTransform.gameObject;
        weapon = throwWeapon;
    }
}