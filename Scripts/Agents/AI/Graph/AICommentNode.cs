using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheBitCave.MMToolsExtensions.AI.Graph
{
    /// <summary>
    /// This node can be used to add comments and notes on the brain graph.
    /// It won't be used during AIBrain generation.
    /// </summary>
    [NodeWidth(250)]
    [CreateNodeMenu("AI/Comment")]
    [NodeTint("#E8DB7D")]
    public class AICommentNode : AINode
    {
        public string comment = C.LABEL_INSERT_COMMENT;
    }
}