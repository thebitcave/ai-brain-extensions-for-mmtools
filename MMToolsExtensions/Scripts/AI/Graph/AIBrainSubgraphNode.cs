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
      
        public AIBrainSubgraph subgraph;

        /// <summary>
        /// State interface list
        /// </summary>
        public List<NodePort> inputStates = new List<NodePort>();

        /// <summary>
        /// Transition interface list
        /// </summary>
        public List<NodePort> outputStates = new List<NodePort>();

        protected override void Init()
        {
            base.Init();
            if (subgraph == null) return;

            GenerateDynamicPorts();
        }

        public void GenerateDynamicPorts()
        {
//            ClearDynamicPorts();
            inputStates = new List<NodePort>();
            foreach (var inNode in subgraph.GetStatesIn())
            {
                var nodeName = inNode.GetPort("input").Connection.node.name;
                nodeName += "-" + C.PORT_IN;

            //    if (HasPort(nodeName)) continue;
                inputStates.Add(AddDynamicInput(typeof(StateConnection), ConnectionType.Multiple, TypeConstraint.Strict, nodeName));
            }

            outputStates = new List<NodePort>();
            foreach (var outNode in subgraph.GetTransitionsOut())
            {
                var nodeName = outNode.GetPort("output").Connection.node.name;
                nodeName += "-" + C.PORT_OUT;
              
            //    if (HasPort(nodeName)) continue;
                outputStates.Add(AddDynamicOutput(typeof(TransitionConnection), ConnectionType.Override, TypeConstraint.Strict, nodeName));
            }
        }
    }
}