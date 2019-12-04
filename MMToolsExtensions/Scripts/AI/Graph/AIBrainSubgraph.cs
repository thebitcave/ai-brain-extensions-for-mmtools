using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XNode;

namespace TheBitCave.MMToolsExtensions.AI.Graph
{
    /// <summary>
    /// A Graph to create AI Brains for the Corgi <see cref="MoreMountains.Tools.AIBrain"/>.
    /// </summary>
    [Serializable, CreateAssetMenu(fileName = "New Brain SubGraph", menuName = "The Bit Cave/AI Brain SubGraph")]
    public class AIBrainSubgraph : NodeGraph, IBrainGraph
    {
        public AIBrainStateNode startingNode;
        
        /// <summary>
        /// Which state should be initialized as the starting one.
        /// </summary>
        public AIBrainStateNode StartingNode
        {
            get => startingNode;
            set => startingNode = value;
        }

        public IEnumerable<AISubgraphStateInNode> GetStatesIn()
        {
            return nodes.OfType<AISubgraphStateInNode>()
                .Select(node => node)
                .ToList();
        }
        
        public IEnumerable<AISubgraphTransitionOutNode> GetTransitionsOut()
        {
            return nodes.OfType<AISubgraphTransitionOutNode>()
                .Select(node => node)
                .ToList();
        }

    }
}