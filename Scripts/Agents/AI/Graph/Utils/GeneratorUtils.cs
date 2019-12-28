using UnityEngine;
using System;
using System.Linq;
using System.Reflection;
using MoreMountains.Tools;

namespace TheBitCave.MMToolsExtensions.AI.Graph
{
    public partial class GeneratorUtils
    {
        public static string GetSubgraphStateName(string subgraphNodeName, string stateName)
        {
            return subgraphNodeName + C.SUBGRAPH_CONNECTION_SYMBOL + stateName;
        }
        
    }
}