using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using XNodeEditor;
using Object = UnityEngine.Object;

namespace TheBitCave.MMToolsExtensions.AI.Graph
{
    [CustomNodeGraphEditor(typeof(AIBrainGraph))]
    public class AIBrainGraphEditor : NodeGraphEditor
    {
        public override string GetNodeMenuName(Type type)
        {
            if (type == typeof(AISubgraphStateInNode)) return null;
            if (type == typeof(AISubgraphTransitionOutNode)) return null;
            return type.IsSubclassOf(typeof(AINode)) ? base.GetNodeMenuName(type) : null;
        }

        public override void OnDropObjects(Object[] objects)
        {
            // Does nothing, but suppresses superclass warnings
            if (objects.OfType<AIBrainSubgraph>().Any())
            {
                EditorApplication.delayCall += RefreshCanvas;
            }
        }

        protected virtual void RefreshCanvas()
        {
            var graph = target as AIBrainGraph;
            if (graph == null) return;
            foreach (var node in graph.nodes)
            {
                if (node is AIBrainSubgraphNode subgraphNode)
                {
                    subgraphNode.GenerateDynamicPorts();
                }
            }
        }
        
        public override void OnOpen()
        {
            window.titleContent.text = target.name;
            RefreshCanvas();
        }
        
        public override void AddContextMenuItems(GenericMenu menu, Type compatibleType = null, XNode.NodePort.IO direction = XNode.NodePort.IO.Input) {
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