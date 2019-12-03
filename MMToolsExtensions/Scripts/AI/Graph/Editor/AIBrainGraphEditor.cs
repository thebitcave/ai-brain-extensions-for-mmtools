using System;
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
            return type.IsSubclassOf(typeof(AINode)) ? base.GetNodeMenuName(type) : null;
        }

        public override void OnDropObjects(Object[] objects)
        {
            // Does nothing, but suppresses superclass warnings
        }

        public override void OnOpen()
        {
            window.titleContent.text = target.name;
        }

    }
}