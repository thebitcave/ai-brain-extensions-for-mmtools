using System;
using UnityEditor;
using UnityEngine;
using XNode;

namespace TheBitCave.MMToolsExtensions.AI.Graph
{
    /// <summary>
    /// A node representing an external connection transition from the subgraph to the parent graph.
    /// </summary>
    [NodeTint("#ABABAB")]
    [CreateNodeMenu("AI/Transition Out")]
    [NodeWidth(180)]
    public class AISubgraphTransitionOutNode : AINode
    {
        /// <summary>
        /// State input for the brain subgraph
        /// </summary>
        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Strict)] public TransitionConnection output;

        protected override void Init()
        {
            base.Init();
            name = C.SUBGRAPH_TRANSITION_OUT;
        }
    }
}