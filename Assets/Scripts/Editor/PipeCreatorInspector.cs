using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PipeCreator))]
public class PipeCreatorInspector : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PipeCreator script = (PipeCreator)target;
        if(GUILayout.Button("Start Operation"))
        {
            script.StartLineOperation();
        }
        if(GUILayout.Button("Finish Operation"))
        {
            script.FinishLineOperation();
        }
    }
}
