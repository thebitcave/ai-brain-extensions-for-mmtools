using System;
using UnityEditor;
using UnityEngine;
using XNodeEditor;
using Object = UnityEngine.Object;

namespace TheBitCave.MMToolsExtensions.AI.Graph
{
    [CustomNodeGraphEditor(typeof(AIBrainSubgraph))]
    public class AIBrainSubgraphEditor : NodeGraphEditor
    {
        public override string GetNodeMenuName(Type type)
        {
            return type.IsSubclassOf(typeof(AINode)) && type != typeof(AIBrainSubgraphNode) ? base.GetNodeMenuName(type) : null;
        }

        public override void OnDropObjects(Object[] objects)
        {
            // Does nothing, but suppresses superclass warnings
        }
        
        public override void OnOpen()
        {
            window.titleContent.text = target.name;
        }

        public override void AddContextMenuItems(GenericMenu menu) {
            base.AddContextMenuItems(menu);
            var graph = target as IBrainGraph;
            menu.AddSeparator("");
            if (graph.IsNodeCollapseModeOn)
            {
                menu.AddItem(new GUIContent("Disable Node Collapsing"), false, () => graph.DisableNodeCollapse());
            }
            else
            {
                menu.AddItem(new GUIContent("Enable Node Collapsing"), false, () => graph.EnableNodeCollapse());
            }
        }
    }
}