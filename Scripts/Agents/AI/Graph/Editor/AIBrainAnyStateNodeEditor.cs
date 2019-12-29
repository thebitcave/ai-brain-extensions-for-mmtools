using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace TheBitCave.MMToolsExtensions.AI.Graph
{
    [CustomNodeEditor(typeof(AIBrainAnyStateNode))]
    public class AIBrainAnyStateNodeEditor : NodeEditor
    {
        private SerializedProperty _transitions;
        private SerializedProperty _canTransitionToSelf;

        public override void OnHeaderGUI()
        {
            GUILayout.Label(C.LABEL_ANY_STATE, NodeEditorResources.styles.nodeHeader, GUILayout.Height(30));
        }
        public override void OnBodyGUI()
        {
            _transitions = serializedObject.FindProperty("transitions");
            _canTransitionToSelf = serializedObject.FindProperty("canTransitionToSelf");

            serializedObject.Update();
            NodeEditorGUILayout.PropertyField(_transitions);
            EditorGUIUtility.labelWidth = 135;
            NodeEditorGUILayout.PropertyField(_canTransitionToSelf);
            serializedObject.ApplyModifiedProperties();
        }

    }
}