using UnityEngine;

namespace TheBitCave.MMToolsExtensions.Tools
{
    /// <summary>
    /// Events used to control AIBrains
    /// </summar>
    public struct AIBrainChangeStateCommandEvent
    {
        // A name used to filter commands from the slave
        public string ChannelName;
        
        // The State the slave brain should enter in
        public string StateName;
        
        // The brain target, if any
        public Transform Target;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="channelName">A name used to filter commands from the slave.</param>
        /// <param name="stateName">The State the slave brain should enter in.</param>
        /// <param name="target">// The brain target, if any.</param>
        public AIBrainChangeStateCommandEvent(string channelName, string stateName, Transform target)
        {
            ChannelName = channelName;
            StateName = stateName;
            Target = target;
        }
    }
}
