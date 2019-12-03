using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace TheBitCave.MMToolsExtensions.AI.Graph
{
    [CustomNodeEditor(typeof(AIBrainSubgraphNode))]
    public class AIBrainSubgraphNodeEditor : NodeEditor
    {
//        private SerializedProperty _statesIn;
        private SerializedProperty _transitions;
        private SerializedProperty _subgraph;
        private SerializedProperty _thePort;

        private AIBrainSubgraphNode _node;
        
        public override void OnCreate()
        {
            base.OnCreate();
            
            _node = target as AIBrainSubgraphNode;
        }

        public override void OnBodyGUI()
        {
//            _statesIn = serializedObject.FindProperty("statesIn");
            _transitions = serializedObject.FindProperty("transitions");
            _subgraph = serializedObject.FindProperty("subgraph");
            _thePort = serializedObject.FindProperty("thePort");

            serializedObject.Update();
//            NodeEditorGUILayout.PropertyField(_statesIn);
            NodeEditorGUILayout.PropertyField(_transitions);
            EditorGUIUtility.labelWidth = 60;
            foreach (var stateIn in _node.inputStates)
            {
                NodeEditorGUILayout.PortField(stateIn);
            }
            EditorGUILayout.Space();
            NodeEditorGUILayout.PropertyField(_subgraph);
            serializedObject.ApplyModifiedProperties();
        }
    }
}