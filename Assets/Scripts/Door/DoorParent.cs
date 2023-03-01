#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class DoorParent : MonoBehaviour
{
    //Left
    [Header("Left")] public DoorChild leftDoor;

    //Right
    [Space, Header("Right")] public DoorChild rightDoor;

    private bool used;

    private void Start()
    {
        if (leftDoor != null)
            leftDoor.SetParent(this);

        if (rightDoor != null)
            rightDoor.SetParent(this);

        RefreshDoor();
    }

    public void RefreshDoor()
    {
        RefreshChild(leftDoor);
        RefreshChild(rightDoor);
    }

    private void RefreshChild(DoorChild child)
    {
        if (child == null) return;

        var doorType = child.type;

        var doorImage = doorType.IsImageDoor();

        child.title.text = doorType.GetTitle();
        child.text.text = doorType.GetTypeText();
        child.meshRenderer.material = doorType.GetMaterial();
        child.bodyImage.color = (doorImage) ? Color.clear : doorType.GetBodyColor();
        child.image.sprite = (doorImage) ? doorType.GetImageDoorSprite() : null;
        child.image.color = (doorImage) ? Color.white : Color.clear;
    }

    public void Trigger(DoorChild child)
    {
        if (used) return;

        var type = child.type;

        if (type.IsPositiveDoor())
        {
            var playerManager = PlayerManager.Instance;
            playerManager.PlayPlayerParticle();
        }

        GameManager.Instance.Vibrate();
        
        type.ApplyEffect();
        
        DeActivateDoors();
    }

    private void DeActivateDoors()
    {
        used = true;

        gameObject.SetActive(false);
    }
}