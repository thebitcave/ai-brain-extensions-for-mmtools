using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace TheBitCave.MMToolsExtensions.AI.Graph
{
    [CustomNodeEditor(typeof(AIBrainAnyStateNode))]
    public class AIBrainAnyStateNodeEditor : NodeEditor
    {
        private SerializedProperty _transitions;

        public override void OnHeaderGUI()
        {
            GUILayout.Label(C.LABEL_ANY_STATE, NodeEditorResources.styles.nodeHeader, GUILayout.Height(30));
        }
        public override void OnBodyGUI()
        {
            _transitions = serializedObject.FindProperty("transitions");

            serializedObject.Update();
            NodeEditorGUILayout.PropertyField(_transitions);
            serializedObject.ApplyModifiedProperties();
        }

    }
}