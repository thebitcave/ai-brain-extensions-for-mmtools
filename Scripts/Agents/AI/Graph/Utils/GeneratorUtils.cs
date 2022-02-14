using UnityEngine;

namespace TheBitCave.MMToolsExtensions.AI.Graph
{
    public partial class GeneratorUtils
    {
        public static string  GetSubgraphStateName(string subgraphNodeName, string stateName)
        {
            if (string.IsNullOrEmpty(stateName)) return "";
            return subgraphNodeName + C.SUBGRAPH_CONNECTION_SYMBOL + stateName;
        }
        
    }
}