using UnityEngine;
using XNodeEditor;

namespace TheBitCave.MMToolsExtensions.AI.Graph
{
    [CustomNodeEditor(typeof(AIBrainStateNode))]
    public class AIBrainStateNodeEditor : NodeEditor
    {
        private AIBrainStateNode _node;

        public override void OnCreate()
        {
            base.OnCreate();

            _node = target as AIBrainStateNode;
        }

        public override void OnHeaderGUI()
        {
            base.OnHeaderGUI();
        }
        
        public override Color GetTint()
        {
            if (!(_node.graph is IBrainGraph graph)) return base.GetTint();

            // TODO
            var c = new Color();
            ColorUtility.TryParseHtmlString("#E63946", out c);

            return ReferenceEquals(graph.StartingNode, _node) ? c : base.GetTint();
        }

        public override void OnBodyGUI()
        {
            base.OnBodyGUI();

            if (_node == null) return;

            if (!(_node.graph is IBrainGraph graph)) return;
            if (!ReferenceEquals(graph.StartingNode, _node) && GUILayout.Button(C.LABEL_SET_AS_STARTING_STATE)) graph.StartingNode = _node;
        }
    }
}