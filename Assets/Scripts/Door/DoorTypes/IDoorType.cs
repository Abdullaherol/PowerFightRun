using UnityEngine;

public interface IDoorType
{
    void ApplyEffect();

    bool IsPositiveDoor();

    string GetTypeText();
    
    string GetTitle();

    Material GetMaterial();

    Color GetBodyColor();

    bool IsImageDoor();

    Sprite GetImageDoorSprite();

    float GetMultiplier();

    DoorType GetDoorType();
}