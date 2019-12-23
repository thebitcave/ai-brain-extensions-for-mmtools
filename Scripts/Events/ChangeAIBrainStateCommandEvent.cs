using UnityEngine;

namespace TheBitCave.MMToolsExtensions.Tools
{
    /// <summary>
    /// Events used to control AIBrains
    /// </summar>
    public struct ChangeAIBrainStateCommandEvent
    {
        // A name used to filter commands from the slave
        public string ChannelName;
        
        // The State the slave brain should enter in
        public string StateName;
        
        // The brain target, if any
        public Transform Target;

        // The sending gameObject
        public GameObject Master;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="channelName">A name used to filter commands from the slave.</param>
        /// <param name="stateName">The State the slave brain should enter in.</param>
        /// <param name="target">The brain target, if any.</param>
        /// <param name="master">The sending gameObject.</param>
        public ChangeAIBrainStateCommandEvent(string channelName, string stateName, Transform target, GameObject master = null)
        {
            ChannelName = channelName;
            StateName = stateName;
            Target = target;
            Master = master;
        }
    }
}
