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
 
        public override void OnHeaderGUI() {
            GUI.color = Color.white;

            if (_node == null) return;

            if (!(_node.graph is IBrainGraph graph)) return;
            
            var title = target.name;
            if (ReferenceEquals(graph.StartingNode, _node))
            {
                title = "[>>] " + target.name;
            }
            else
            {
                GUI.color = new Color(.8f, .8f, .8f);
            }
            GUILayout.Label(title, NodeEditorResources.styles.nodeHeader, GUILayout.Height(30));
            
            GUI.color = Color.white;
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