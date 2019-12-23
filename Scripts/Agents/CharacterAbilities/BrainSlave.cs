using System.Collections.Generic;
using System.Linq;
using MoreMountains.Tools;
using TheBitCave.MMToolsExtensions.Tools;
using UnityEngine;
using UnityEngine.Serialization;

namespace TheBitCave.MMToolsExtensions
{
    /// <summary>
    /// This component is used to control the <see cref="MoreMountains.Tools.AIBrain"/> as a slave from other parts of the application. When receiving an event if a channel corresponds, the brain will transition to the new state.
    /// </summary>
    public class BrainSlave : SimpleAbility, MMEventListener<ChangeAIBrainStateCommandEvent>
    {
        public override string HelpBoxText() { return "This component is used to control the AIBrain as a slave from other parts of the application. When receiving an event, if a channel corresponds the brain will transition to the new state."; }

        [Header("Slave Brain Settings")]
        // A list of identifiers used to filter commands from the master brain (if a channel name corresponds, the brain will transition to the desired state).
        public List<string> ChannelNames;

        // The Slave will also listen for commands sent by the same gameObject.
        public bool executeSelfSentCommands;
        
        private AIBrain _aiBrain;
        
        protected override void Start()
        {
            _aiBrain = GetComponent<AIBrain>();
            if (_aiBrain == null)
            {
                Debug.LogError("CharacterBrainSlave needs an AIBrain: please add one to the character.");
            }
        }

        /// <summary>
        /// If the channel sent by the command corresponds and the state exists,
        /// forces state transition.
        /// </summary>
        /// <param name="changeAiBrainStateCommandEvent"></param>
        public virtual void OnMMEvent(ChangeAIBrainStateCommandEvent changeAiBrainStateCommandEvent)
        {
            if (!executeSelfSentCommands && changeAiBrainStateCommandEvent.Master == gameObject) return;
            if (ChannelNames.Any(channelName => changeAiBrainStateCommandEvent.ChannelName == channelName))
            {
                TransitionToState(changeAiBrainStateCommandEvent.StateName, changeAiBrainStateCommandEvent.Target);
            }
        }

        /// <summary>
        /// Sets the AIBrain to a new state
        /// </summary>
        /// <param name="newStateName">The new state name</param>
        /// <param name="target">The brain target (if any)</param>
        public virtual void TransitionToState(string newStateName, Transform target)
        {
            var hasState = _aiBrain.States.Any(state => state.StateName == newStateName);
            if (!hasState) return;
            _aiBrain.Target = target;
            _aiBrain.TransitionToState(newStateName);
            PlayAbilityFeedbacks();
        }
        
        protected override void OnEnable()
        {
            base.OnEnable();
            this.MMEventStartListening<ChangeAIBrainStateCommandEvent>();
        }
        
        protected override void OnDisable()
        {
            base.OnDisable();
            this.MMEventStopListening<ChangeAIBrainStateCommandEvent>();
        }
    }
}