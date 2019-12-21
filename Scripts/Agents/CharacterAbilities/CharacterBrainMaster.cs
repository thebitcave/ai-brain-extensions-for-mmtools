using MoreMountains.CorgiEngine;
using TheBitCave.MMToolsExtensions.Tools;
using MoreMountains.Tools;
using TheBitCave.MMToolsExtensions;
using TheBitCave.MMToolsExtensions.AI;
using UnityEngine;

namespace TheBitCave.MMToolsExtensions
{
    /// <summary>
    /// This component allows your character to broadcast messages that will be caught and executed by AI characters with an <see cref="TheBitCave.MMToolsExtensions.CharacterBrainSlave"/> enabled.
    /// </summary>
    public class CharacterBrainMaster : CharacterSimpleAbility
    {
        public override string HelpBoxText() { return "This component allows your character to broadcast messages that will be caught and executed by AI characters with an AIBrainSlave enabled."; }

        /// <summary>
        /// We broadcast a notification to all AIBrain Slaves
        /// </summary>
        /// <param name="channelName">An id used to filter messages</param>
        /// <param name="stateName">The state the brain should transition to</param>
        /// <param name="target">The brain target (if any)</param>
        public virtual void SendCommand(string channelName, string stateName, Transform target)
        {
            var evt = new ChangeAIBrainStateCommandEvent(channelName, stateName, target);
            MMEventManager.TriggerEvent(evt);
            PlayAbilityFeedbacks();
        }
    }
}