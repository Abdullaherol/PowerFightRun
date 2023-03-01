using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DoorManager))]
public class DoorManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        if (GUILayout.Button("Refresh Doors"))
        {
            foreach (var door in FindObjectsOfType<DoorParent>())
            {
                door.RefreshDoor();
            }
        }
    }
}