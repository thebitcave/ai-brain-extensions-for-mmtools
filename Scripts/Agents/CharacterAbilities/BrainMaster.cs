using TheBitCave.MMToolsExtensions.Tools;
using MoreMountains.Tools;
using UnityEngine;

namespace TheBitCave.MMToolsExtensions
{
    /// <summary>
    /// This component allows your character to broadcast messages that will be caught and executed by AI characters with an <see cref="BrainSlave"/> enabled.
    /// </summary>
    public class BrainMaster : SimpleAbility
    {
        public override string HelpBoxText() { return "This component allows your character to broadcast messages that will be caught and executed by AI characters with an AIBrainSlave enabled."; }

        /// <summary>
        /// We broadcast a notification to all AIBrain Slaves
        /// </summary>
        /// <param name="channel">An id used to filter messages</param>
        /// <param name="newStateName">The state the brain should transition to</param>
        /// <param name="target">The brain target (if any)</param>
        public virtual void SendCommand(StateCommandChannel channel, string newStateName, Transform target = null)
        {
            var evt = new ChangeAIBrainStateCommandEvent(channel, newStateName, target, gameObject);
            MMEventManager.TriggerEvent(evt);
            PlayAbilityFeedbacks();
        }
    }
}