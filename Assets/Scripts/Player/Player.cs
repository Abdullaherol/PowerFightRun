using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform weaponParent;
    public GameObject currentWeapon;
    public ThrowWeapon weapon;
    public ParticleSystem particleChangeWeapon;
    public Animator animatorParent;
    public Animator animatorCharacter;

    public void ChangeWeapon(ThrowWeapon throwWeapon, bool playParticle)
    {
        if (currentWeapon != null)
        {
            if (weapon != throwWeapon)
                Destroy(currentWeapon);
            else return;
        }

        if (playParticle)
            particleChangeWeapon.Play();
        
        animatorParent.Play("ChangeWeapon");

        var weaponTransform = Instantiate(throwWeapon.prefab).transform;

        weaponTransform.parent = weaponParent;
        weaponTransform.localPosition = throwWeapon.position;
        weaponTransform.localEulerAngles = throwWeapon.rotation;
        weaponTransform.localScale = throwWeapon.scale;

        currentWeapon = weaponTransform.gameObject;
        weapon = throwWeapon;
    }
}