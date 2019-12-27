using System.Linq;
using UnityEngine;
using XNodeEditor;

namespace TheBitCave.MMToolsExtensions.AI.Graph
{
    [CustomNodeEditor(typeof(AIBrainStateAliasNode))]
    public class AIBrainStateAliasNodeEditor : NodeEditor
    {
        protected AIBrainStateAliasNode _node;

        public override void OnCreate()
        {
            base.OnCreate();
            _node = target as AIBrainStateAliasNode;
        }

        public override void OnHeaderGUI()
        {
            GUILayout.Label(C.LABEL_STATE_ALIAS, NodeEditorResources.styles.nodeHeader, GUILayout.Height(30));
        }
        
        public override void OnBodyGUI()
        {
            foreach (var node in _node.graph.nodes.OfType<AIBrainStateNode>())
            {
                Debug.Log("TODO: add list element " + node.name);
            }
        }

    }
}