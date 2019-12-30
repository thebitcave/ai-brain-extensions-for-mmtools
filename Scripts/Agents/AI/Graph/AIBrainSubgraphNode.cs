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
    [CreateNodeMenu("AI/Brain SubGraph")]
    [NodeWidth(200)]
    public class AIBrainSubgraphNode : AIStartSelectableNode
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

        public virtual void GenerateDynamicPorts()
        {
//            ClearDynamicPorts();
            inputStates = new List<NodePort>();
            foreach (var inNode in subgraph.GetStatesIn())
            {
                var fieldName = inNode.GetPort(C.PORT_INPUT).Connection.node.name;
                fieldName += "-" + C.PORT_IN;

                var inputState = AddDynamicInput(typeof(StateConnection), ConnectionType.Multiple, TypeConstraint.Strict, fieldName);
                if(!inputStates.Contains(inputState)) inputStates.Add(inputState);
            }

            outputStates = new List<NodePort>();
            foreach (var outNode in subgraph.GetTransitionsOut())
            {
                var fieldName = outNode.GetPort(C.PORT_OUTPUT).Connection.node.name;
                fieldName += "-" + C.PORT_OUT;

                var outputState = AddDynamicOutput(typeof(TransitionConnection), ConnectionType.Override, TypeConstraint.Strict, fieldName);
                if(!outputStates.Contains(outputState)) outputStates.Add(outputState);
            }
        }

        public AIBrainGraph ParentGraph => graph as AIBrainGraph;
    }
}