namespace TheBitCave.MMToolsExtensions.AI.Graph
{
    /// <summary>
    /// A node representing an external connection state from the subgraph to the parent graph.
    /// </summary>
    [NodeTint("#BABABA")]
    [CreateNodeMenu("AI/State In")]
    [NodeWidth(180)]
    public class AISubgraphStateInNode : AINode
    {
        /// <summary>
        /// State input for the brain subgraph
        /// </summary>
        [Output(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Strict)] public StateConnection input;

        protected override void Init()
        {
            base.Init();
            name = C.SUBGRAPH_STATE_IN;
        }
    }
}