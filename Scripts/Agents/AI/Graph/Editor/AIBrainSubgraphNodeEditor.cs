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
 
        public override Color GetTint()
        {
            if (!(_node.graph is IBrainGraph graph)) return base.GetTint();
            ColorUtility.TryParseHtmlString(C.COLOR_STARTING_STATE, out var c);
            return ReferenceEquals(graph.StartingNode, _node) ? c : base.GetTint();
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
            
            if (!(_node.graph is IBrainGraph graph)) return;
            if (!ReferenceEquals(graph.StartingNode, _node) && GUILayout.Button(C.LABEL_SET_AS_STARTING_STATE)) graph.StartingNode = _node;

        }
    }
}