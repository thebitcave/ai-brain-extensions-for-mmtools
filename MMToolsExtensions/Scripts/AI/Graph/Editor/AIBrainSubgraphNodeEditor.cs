using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace TheBitCave.MMToolsExtensions.AI.Graph
{
    [CustomNodeEditor(typeof(AIBrainSubgraphNode))]
    public class AIBrainSubgraphNodeEditor : NodeEditor
    {
        private SerializedProperty _statesIn;
        private SerializedProperty _transitions;
        private SerializedProperty _subgraph;

        public override void OnBodyGUI()
        {
            _statesIn = serializedObject.FindProperty("statesIn");
            _transitions = serializedObject.FindProperty("transitions");
            _subgraph = serializedObject.FindProperty("subgraph");

            serializedObject.Update();
            NodeEditorGUILayout.PropertyField(_statesIn);
            NodeEditorGUILayout.PropertyField(_transitions);
            EditorGUIUtility.labelWidth = 60;
            NodeEditorGUILayout.PropertyField(_subgraph);
            serializedObject.ApplyModifiedProperties();
        }
    }
}