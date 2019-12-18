using System.Linq;
using MoreMountains.Tools;
using TheBitCave.MMToolsExtensions.Tools;
using UnityEngine;

namespace TheBitCave.MMToolsExtensions.AI
{
    /// <summary>
    /// A component used to control the Corgi <see cref="MoreMountains.Tools.AIBrain"/> as a slave from
    /// another (master) AIBrain. 
    /// </summary>
    [RequireComponent(typeof(AIBrain))]
    public class AIBrainSlave : MonoBehaviour, MMEventListener<AIBrainChangeStateCommandEvent>
    {
        // An identifier used to filter commands from the master slave (if the channel name corresponds, the brain will transition to the desired state).
        public string ChannelName;
        
        private AIBrain _aiBrain;
        
        private void Start()
        {
            _aiBrain = GetComponent<AIBrain>();
        }

        /// <summary>
        /// If the channel sent by the command corresponds and the state exists,
        /// forces state transition.
        /// </summary>
        /// <param name="aiBrainChangeStateCommandEvent"></param>
        public virtual void OnMMEvent(AIBrainChangeStateCommandEvent aiBrainChangeStateCommandEvent)
        {
            if (aiBrainChangeStateCommandEvent.ChannelName != ChannelName) return;
            var hasState = _aiBrain.States.Any(state => state.StateName == aiBrainChangeStateCommandEvent.StateName);
            if (!hasState) return;
            _aiBrain.Target = aiBrainChangeStateCommandEvent.Target;
            _aiBrain.TransitionToState(aiBrainChangeStateCommandEvent.StateName);
        }
        
        private void OnEnable()
        {
            this.MMEventStartListening<AIBrainChangeStateCommandEvent>();
        }
        
        private void OnDisable()
        {
            this.MMEventStopListening<AIBrainChangeStateCommandEvent>();
        }
    }
}