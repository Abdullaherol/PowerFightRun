using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PodiumHelper))]
public class PodiumHelperEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var podiumHelper = (PodiumHelper)target;
        
        if (GUILayout.Button("Spawn"))
        {
            podiumHelper.Spawn();
        }

        if (GUILayout.Button("Remove"))
        {
            podiumHelper.Remove();
        }
    }
}