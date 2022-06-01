
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
//using GAG.Scene.Runtime;

[CustomEditor(typeof(BoomShake))]
[CanEditMultipleObjects]
public class BoomShakeEditor : Editor
{

    void OnEnable()
    {
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        BoomShake myScript = (BoomShake)target;

        EditorGUILayout.Space();

        if (GUILayout.Button("Shake"))
        {
            myScript.StartShake();
        }
    }
}
#endif
