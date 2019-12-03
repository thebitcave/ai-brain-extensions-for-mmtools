using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Serialization;
using XNode;
using Random = System.Random;

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
        /// Transitions to exit this state
        /// </summary>
        [Output(connectionType = ConnectionType.Multiple)] public TransitionConnection transitions;
        
        [Header("Subgraph Assets")]
        public AIBrainSubgraph subgraph;

        public List<NodePort> inputStates = new List<NodePort>();

        protected override void Init()
        {
            base.Init();
            inputStates = new List<NodePort>();
            if (subgraph == null) return;
            foreach (var t in subgraph.GetStatesIn())
            {
                inputStates.Add(this.AddDynamicInput(typeof(StateConnection), ConnectionType.Override, TypeConstraint.Strict, "thePort" + UnityEngine.Random.Range(0, 1.5f)));
            }
        }
    }
}