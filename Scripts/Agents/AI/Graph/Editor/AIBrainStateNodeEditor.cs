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

        public override void OnBodyGUI() {
            base.OnBodyGUI();
            
            if (_node == null) return;

            if (!(_node.graph is IBrainGraph graph)) return;
            if (!ReferenceEquals(graph.StartingNode, _node) && GUILayout.Button(C.LABEL_SET_AS_STARTING_STATE)) graph.StartingNode = _node;
        }
    }
}