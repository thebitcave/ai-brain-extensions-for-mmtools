namespace TheBitCave.MMToolsExtensions.AI.Graph
{
    /// <summary>
    /// A node representing an alias for the <see cref="TheBitCave.MMToolsExtensions.AI.Graph.AIBrainStateNode"/>.
    /// It is used to create shortcuts and make the graph more readable.
    /// </summary>
    [NodeTint("#E63946")]
    [CreateNodeMenu("AI/Brain State Alias")]
    [NodeWidth(180)]
    public class AIBrainStateAliasNode : AINode
    {
        /// <summary>
        /// Input states (i.e.: where we came from?)
        /// </summary>
        [Input(connectionType = ConnectionType.Multiple, typeConstraint = TypeConstraint.Strict)] public StateConnection statesIn;

    }
}