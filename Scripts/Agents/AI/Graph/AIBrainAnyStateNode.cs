namespace TheBitCave.MMToolsExtensions.AI.Graph
{
    /// <summary>
    /// A node representing a generic state in the MMTools <see cref="MoreMountains.Tools.AIBrain"/>.
    /// It is used to generate common decision logic in all states.
    /// </summary>
    [NodeTint("#E63946")]
    [CreateNodeMenu("AI/Brain Any State")]
    [NodeWidth(190)]
    public class AIBrainAnyStateNode : AINode
    {
        /// <summary>
        /// Transitions to exit this state
        /// </summary>
        [Output(connectionType = ConnectionType.Multiple)] public TransitionConnection transitions;
    }
}