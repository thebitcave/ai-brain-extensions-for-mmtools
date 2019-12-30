using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace TheBitCave.MMToolsExtensions.AI.Graph
{
    [CustomNodeEditor(typeof(AIBrainStateNode))]
    public class AIBrainStateNodeEditor : NodeEditor
    {
        private AIBrainStateNode _node;

        private SerializedProperty _canTransitionToSelf;

        public override void OnCreate()
        {
            base.OnCreate();

            _node = target as AIBrainStateNode;
        }

        public override Color GetTint()
        {
            if (!(_node.graph is IBrainGraph graph)) return base.GetTint();
            ColorUtility.TryParseHtmlString(C.COLOR_STARTING_STATE, out var c);
            return ReferenceEquals(graph.StartingNode, _node) ? c : base.GetTint();
        }

        public override void OnBodyGUI()
        {
            EditorGUIUtility.labelWidth = 155;
            base.OnBodyGUI();

            _canTransitionToSelf = serializedObject.FindProperty("canTransitionToSelf");
            
            if (_node == null) return;
            if (!(_node.graph is IBrainGraph graph)) return;
            if (!ReferenceEquals(graph.StartingNode, _node) && GUILayout.Button(C.LABEL_SET_AS_STARTING_STATE)) graph.StartingNode = _node;
        }
    }
}