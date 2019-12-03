using System;
using UnityEditor;
using UnityEngine;
using XNode;

namespace TheBitCave.MMToolsExtensions.AI.Graph
{
    /// <summary>
    /// A node representing a brain subgraph.
    /// </summary>
    [NodeTint("#E95957")]
    [CreateNodeMenu("AI/Brain SubGraph")]
    [NodeWidth(200)]
    public class AIBrainSubgraphNode : AINode
    {
        /// <summary>
        /// Input states (i.e.: where we came from?)
        /// </summary>
        [Input(connectionType = ConnectionType.Multiple, typeConstraint = TypeConstraint.Strict)] public StateConnection statesIn;
        
        /// <summary>
        /// Transitions to exit this state
        /// </summary>
        [Output(connectionType = ConnectionType.Multiple)] public TransitionConnection transitions;
        
        public AIBrainSubgraph subgraph;

    }
}