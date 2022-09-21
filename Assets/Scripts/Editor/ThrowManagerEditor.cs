using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ThrowManager))]
public class ThrowManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ThrowManager throwManager = (ThrowManager)target;

        if (GUILayout.Button("Increase Bullet"))
        {
            throwManager.upgrade.bulletCount++;
        }

        if (GUILayout.Button("Decrease Bullet"))
        {
            if (throwManager.upgrade.bulletCount > 0)
                throwManager.upgrade.bulletCount--;
        }
        
        if (GUILayout.Button("Increase Fire Rate"))
        {
            throwManager.upgrade.fireRate -= .1f;
        }
        
        if (GUILayout.Button("Decrease Fire Rate"))
        {
            throwManager.upgrade.fireRate += .1f;
        }
    }
}