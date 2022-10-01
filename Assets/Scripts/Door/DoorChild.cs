using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class DoorChild : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public TMPro.TextMeshProUGUI title;
    public TMPro.TextMeshProUGUI text;
    public Image bodyImage;
    public Image image;

    private DoorParent _parent;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _parent.Trigger(this);
        }
    }

    public void SetParent(DoorParent parent)
    {
        _parent = parent;
    }
}