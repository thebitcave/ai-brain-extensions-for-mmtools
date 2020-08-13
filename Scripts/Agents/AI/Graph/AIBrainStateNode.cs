namespace TheBitCave.MMToolsExtensions.AI.Graph
{
	/// <summary>
	/// A node representing a single state in the MMTools <see cref="MoreMountains.Tools.AIBrain"/>.
	/// </summary>
	[CreateNodeMenu("AI/Brain State")]
	[NodeWidth(200)]
	public class AIBrainStateNode : AIStartSelectableNode
	{
		/// <summary>
		/// Input states (i.e.: where we came from?)
		/// </summary>
		[Input(connectionType = ConnectionType.Multiple, typeConstraint = TypeConstraint.Strict)] public StateConnection statesIn;

		/// <summary>
		/// Actions to be performed when entering this state
		/// </summary>
		[Input(connectionType = ConnectionType.Multiple, typeConstraint = TypeConstraint.Strict)] public ActionConnection actions;

		/// <summary>
		/// Transitions to exit this state
		/// </summary>
		[Output(connectionType = ConnectionType.Multiple)] public TransitionConnection transitions;

		/// <summary>
		/// If set to false, will clean up all self-transitions (for instance Idle to Idle).
		/// </summary>
		public bool canTransitionToSelf = true;
	}
}