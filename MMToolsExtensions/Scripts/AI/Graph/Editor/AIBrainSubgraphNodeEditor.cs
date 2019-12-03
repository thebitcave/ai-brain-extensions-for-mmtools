using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace TheBitCave.MMToolsExtensions.AI.Graph
{
    [CustomNodeEditor(typeof(AIBrainSubgraphNode))]
    public class AIBrainSubgraphNodeEditor : NodeEditor
    {
        private SerializedProperty _subgraph;
        private AIBrainSubgraphNode _node;
        
        public override void OnCreate()
        {
            base.OnCreate();
            
            _node = target as AIBrainSubgraphNode;
        }

        public override void OnBodyGUI()
        {
            _subgraph = serializedObject.FindProperty("subgraph");

            serializedObject.Update();
            EditorGUIUtility.labelWidth = 60;
            foreach (var stateIn in _node.inputStates)
            {
                NodeEditorGUILayout.PortField(stateIn);
            }
            foreach (var stateOut in _node.outputStates)
            {
                NodeEditorGUILayout.PortField(stateOut);
            }
            EditorGUILayout.Space();
            NodeEditorGUILayout.PropertyField(_subgraph);
            serializedObject.ApplyModifiedProperties();
        }
    }
}