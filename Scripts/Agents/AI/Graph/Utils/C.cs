namespace TheBitCave.MMToolsExtensions.AI.Graph
{
    /// <summary>
    /// A list of utility constants
    /// </summary>
    public static class C
    {
        // Graph labels
        public const string AI_BRAIN_GRAPH_LABEL           = "AI Brain Graph";
        public const string AI_BRAIN_SUBGRAPH_LABEL        = "AI Brain SubGraph";
        public const string SUBGRAPH_STATE_IN              = "State In";
        public const string SUBGRAPH_TRANSITION_OUT        = "Transition Out";
                 
        // Port names
        public const string PORT_OUTPUT                    = "output";
        public const string PORT_INPUT                     = "input";
        public const string PORT_ACTIONS                   = "actions";
        public const string PORT_TRUE_STATE                = "trueState";
        public const string PORT_FALSE_STATE               = "falseState";
        public const string PORT_INPUT_STATE               = "inputState";
        public const string PORT_STATES_IN                 = "statesIn";
        public const string PORT_TRANSITION                = "transition";
        public const string PORT_TRANSITIONS               = "transitions";
        public const string PORT_DECISION                  = "decision";
        public const string PORT_IN                        = "in";
        public const string PORT_OUT                       = "out";
        
        // Error Messages
        public const string ERROR_NO_AI_BRAIN = "No AI Brain has been set in Brain Generator.";
        public const string ERROR_DUPLICATE_STATE_NAMES = "Duplicate AI brain state names. Generation aborted.";

        // Warning Messages
        public const string WARNING_GENERATE_SCRIPTS = "Generating the AI will remove all AI Brain, Action and Decision scripts present attached to this gameobject!";

        // Labels
        public const string LABEL_SET_AS_STARTING_STATE     = "Set as starting state";
        public const string LABEL_GENERATE                  = "Generate";
        public const string LABEL_REMOVE_AI_SCRIPTS         = "Remove AI Scripts";
        public const string LABEL_TRANSITION                = "Transition";
        public const string LABEL_SELECTED_GRAPH            = "Selected Graph";
        public const string LABEL_COMMENT                   = "Comment";
        public const string LABEL_ANY_STATE                 = "Any State";
        public const string LABEL_STATE_ALIAS               = "State Alias";

        // AI Brain Debugger Settings

        public const string DEBUG_NO_AIBRAIN_COMPONENT = "Please select a GameObject in scene with an AIBrainDebuggable component attached.";
        public const string DEBUG_APPLICATION_NOT_PLAYING = "Application is not playing: hit Play button to start debugging.";
        public const string DEBUG_GAMEOBJECT_DISABLED = "GameObject is disabled: please enable it to start debugging.";
        public const string DEBUG_SELECTED_BRAIN_LABEL = "Debugging AIBrain for ";
        public const string DEBUG_BRAIN_IS_LABEL = "Brain is ";
        public const string DEBUG_ACTIVE_LABEL = "active";
        public const string DEBUG_INACTIVE_LABEL = "inactive";
        public const string DEBUG_CURRENT_STATE_LABEL = "Current State";
        public const string DEBUG_PREVIOUS_STATE_LABEL = "Previous State";
        public const string DEBUG_TIME_IN_STATE_LABEL = "Time in State";
        public const string DEBUG_TARGET_LABEL = "Target";
        public const string DEBUG_TARGET_NULL_LABEL = "Target is null";
        public const string DEBUG_SET_LABEL = "Set";
        public const string DEBUG_REMOVE_LABEL = "Remove";
        public const string DEBUG_PERFORMING_LABEL = "Preforming";
        public const string DEBUG_STATES_LABEL = "States";
        public const string DEBUG_STATE_TRANSITIONS_LABEL = "State Transitions";
        public const string DEBUG_SET_TARGET_LABEL = "Set Target";
    }
}