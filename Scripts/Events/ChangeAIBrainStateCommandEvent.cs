using UnityEngine;

namespace TheBitCave.MMToolsExtensions.Tools
{
    /// <summary>
    /// Events used to control AIBrains
    /// </summar>
    public struct ChangeAIBrainStateCommandEvent
    {
        // Data used to filter commands from the slave
        public StateCommandChannel Channel;
        
        // The State the slave brain should enter in
        public string StateName;
        
        // The brain target, if any
        public Transform Target;

        // The sending gameObject
        public GameObject Master;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="channel">Data used to filter commands from the slave.</param>
        /// <param name="stateName">The State the slave brain should enter in.</param>
        /// <param name="target">The brain target, if any.</param>
        /// <param name="master">The sending gameObject.</param>
        public ChangeAIBrainStateCommandEvent(StateCommandChannel channel, string stateName, Transform target, GameObject master = null)
        {
            Channel = channel;
            StateName = stateName;
            Target = target;
            Master = master;
        }
    }
}
