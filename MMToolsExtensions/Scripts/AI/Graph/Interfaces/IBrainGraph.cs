using System;
using UnityEngine;
using XNode;

namespace TheBitCave.MMToolsExtensions.AI.Graph
{
    public interface IBrainGraph
    {
        AIBrainStateNode StartingNode { get; set; }
    }
}