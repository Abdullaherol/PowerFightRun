using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerManager))]
public class PlayerManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PlayerManager playerManager = (PlayerManager)target;

        if (GUILayout.Button("Increase Player"))
        {
            playerManager.IncreasePlayer();
        }

        if (playerManager.playerCount > 1 && GUILayout.Button("Decrease Player"))
        {
            playerManager.DecreasePlayer();
        }
    }
}