using System;
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
    }
}