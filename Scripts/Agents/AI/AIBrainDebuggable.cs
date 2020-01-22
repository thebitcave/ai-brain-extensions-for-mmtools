using MoreMountains.Tools;
using UnityEngine;

namespace TheBitCave.MMToolsExtensions.AI
{
    /// <summary>
    /// A subclass to the regular Corgi <see cref="MoreMountains.Tools.AIBrain"/> used
    /// for debugging purposes.
    /// </summary>
    [AddComponentMenu("The Bit Cave/AI/AI Brain Debuggable")]
    public class AIBrainDebuggable : AIBrain
    {

        public delegate void OnPerformingActions(AIActionsList actions);
        public event OnPerformingActions onPerformingActions;

        /// <summary>
        /// Every frame we update our current state
        /// </summary>
        protected override void Update()
        {
            if (!BrainActive || CurrentState == null) return;

            if (Time.time - _lastActionsUpdate > ActionsFrequency)
            {
                onPerformingActions?.Invoke(CurrentState.Actions);

                CurrentState.PerformActions();
                _lastActionsUpdate = Time.time;
            }
            
            if (Time.time - _lastDecisionsUpdate > DecisionFrequency)
            {
                // ***********************
                // TODO List transitions?
                // ***********************
                
                CurrentState.EvaluateTransitions();
                _lastDecisionsUpdate = Time.time;
            }
            
            TimeInThisState += Time.deltaTime;
        }
        
        /// <summary>
        /// When exiting a state we reset our time counter
        /// </summary>
        protected override void OnExitState()
        {
            TimeInPreviousState = TimeInThisState;

            base.OnExitState();
        }
        
        public float TimeInPreviousState { get; private set; }
    }
}
