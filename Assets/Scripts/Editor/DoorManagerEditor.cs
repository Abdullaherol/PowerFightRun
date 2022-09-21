using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DoorManager))]
public class DoorManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        DoorManager doorManager = (DoorManager)target;

        if (GUILayout.Button("Refresh Doors"))
        {
            foreach (var door in GameObject.FindObjectsOfType<DoorParent>())
            {
                door.RefreshDoor(doorManager);
            }
        }
    }
}