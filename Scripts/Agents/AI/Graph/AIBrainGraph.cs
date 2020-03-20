using System;
using UnityEngine;
using XNode;

namespace TheBitCave.MMToolsExtensions.AI.Graph
{
	/// <summary>
	/// A Graph to create AI Brains for the Corgi <see cref="MoreMountains.Tools.AIBrain"/>.
	/// </summary>
	[Serializable, CreateAssetMenu(fileName = "New Brain Graph", menuName = "The Bit Cave/AI Brain Graph")]
	public class AIBrainGraph : NodeGraph, IBrainGraph
	{
		public AIStartSelectableNode startingNode;

		protected bool _nodeCollapseModeOn;
		
		/// <summary>
		/// Which state should be initialized as the starting one.
		/// </summary>
		public AIStartSelectableNode StartingNode
		{
			get => startingNode;
			set => startingNode = value;
		}

		public bool IsNodeCollapseModeOn => _nodeCollapseModeOn;

		/// <summary>
		/// Enables node collapsing.
		/// </summary>
		public void EnableNodeCollapse()
		{
			_nodeCollapseModeOn = true;
		}

		/// <summary>
		/// Disables node collapsing.
		/// </summary>
		public void DisableNodeCollapse()
		{
			_nodeCollapseModeOn = false;
		}
	}	
}
