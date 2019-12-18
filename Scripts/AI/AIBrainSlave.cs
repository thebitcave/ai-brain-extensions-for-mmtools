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
    public class AIBrainSlave : MonoBehaviour, MMEventListener<ChangeAIBrainStateCommandEvent>
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
        /// <param name="changeAiBrainStateCommandEvent"></param>
        public virtual void OnMMEvent(ChangeAIBrainStateCommandEvent changeAiBrainStateCommandEvent)
        {
            if (changeAiBrainStateCommandEvent.ChannelName != ChannelName) return;
            var hasState = _aiBrain.States.Any(state => state.StateName == changeAiBrainStateCommandEvent.StateName);
            if (!hasState) return;
            _aiBrain.Target = changeAiBrainStateCommandEvent.Target;
            _aiBrain.TransitionToState(changeAiBrainStateCommandEvent.StateName);
        }
        
        private void OnEnable()
        {
            this.MMEventStartListening<ChangeAIBrainStateCommandEvent>();
        }
        
        private void OnDisable()
        {
            this.MMEventStopListening<ChangeAIBrainStateCommandEvent>();
        }
    }
}