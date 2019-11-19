namespace TheBitCave.MMToolsExtensions.AI.Graph
{
    /// <summary>
    /// A list of utility constants
    /// </summary>
    public static class C
    {
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
        
        // Error Messages
        public const string ERROR_NO_AI_BRAIN = "No AI Brain has been set in Brain Generator.";
        public const string ERROR_DUPLICATE_STATE_NAMES = "Duplicate AI brain state names. Generation aborted.";

        // Warning Messages
        public const string WARNING_GENERATE_SCRIPTS = "Generating the AI will remove all AI Brain, Action and Decision scripts present attached to this gameobject!";

        // Labels
        public const string LABEL_SET_AS_STARTING_STATE = "Set as starting state";
        public const string LABEL_GENERATE = "Generate";
        public const string LABEL_REMOVE_AI_SCRIPTS = "Remove AI Scripts";
        
        // AI Brain Debugger Settings

        public const string AIBRAIN_DEBUGGER_NO_AIBRAIN_COMPONENT = "Please select a GameObject in scene with an AIBrain Component attached.";
        public const string AIBRAIN_DEBUGGER_APPLICATION_NOT_PLAYING = "Application is not playing: hit Play button to start debugging.";
        public const string AIBRAIN_DEBUGGER_SELECTED_BRAIN_LABEL = "Debugging AIBrain for ";
        public const string AIBRAIN_DEBUGGER_BRAIN_IS_LABEL = "Brain is ";
        public const string AIBRAIN_DEBUGGER_ACTIVE_LABEL = "active";
        public const string AIBRAIN_DEBUGGER_INACTIVE_LABEL = "inactive";
    }
}