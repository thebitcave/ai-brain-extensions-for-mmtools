using System;
using UnityEditor;
using UnityEngine;
using XNode;

namespace TheBitCave.MMToolsExtensions.AI.Graph
{
    /// <summary>
    /// A node representing an external connection state from the subgraph to the parent graph.
    /// </summary>
    [NodeTint("#E63946")]
    [CreateNodeMenu("AI/State In")]
    [NodeWidth(180)]
    public class AIStateInNode : AINode
    {
        /// <summary>
        /// State input for the brain subgraph
        /// </summary>
        [Output(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Strict)] public StateConnection transitions;

    }
}