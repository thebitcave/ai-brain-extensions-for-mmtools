using MoreMountains.Tools;
using UnityEngine;

namespace TheBitCave.MMToolsExtensions.AI
{
    /// <summary>
    /// This action sends a change state command to all registered slave brains
    /// </summary>
    [RequireComponent(typeof(BrainMaster))]
    public class AIActionChangeAIBrainStateCommand : AIAction
    {
        [Header("Master Brain Settings")]
        // The channel used to filter which slave should perform the action.
        public StateCommandChannel Channel;

        // The state the slave should enter in.
        public string StateName;

        protected BrainMaster _ability;

        protected override void Start()
        {
            base.Start();
            _ability = GetComponent<BrainMaster>();
        }

        /// <summary>
        /// On PerformAction we send a command to all brain slaves
        /// </summary>
        public override void PerformAction()
        {
            _ability.SendCommand(Channel, StateName, _brain.Target);
        }
    }
}