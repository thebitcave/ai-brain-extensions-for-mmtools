using UnityEngine;
using XNodeEditor;

namespace TheBitCave.MMToolsExtensions.AI.Graph
{
    [CustomNodeEditor(typeof(AIBrainStateNode))]
    public class AIBrainStateNodeEditor : NodeEditor
    {
        public override void OnHeaderGUI() {
            GUI.color = Color.white;

            var node = target as AIBrainStateNode;
            if (node == null) return;
            
            var graph = node.graph as IBrainGraph;
            if (graph == null) return;
            
            var title = target.name;
            if (graph.StartingNode == node)
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
            
            var node = target as AIBrainStateNode;
            if (node == null) return;

            if (!(node.graph is IBrainGraph graph)) return;
            
            if (graph.StartingNode != node && GUILayout.Button(C.LABEL_SET_AS_STARTING_STATE)) graph.StartingNode = node;
        }

    }
}