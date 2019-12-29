using System.Collections.Generic;
using System.Linq;
using MoreMountains.Tools;
using UnityEngine;
using XNode;
using Object = UnityEngine.Object;

namespace TheBitCave.MMToolsExtensions.AI.Graph
{
    public partial class GeneratorUtils
    {
        private readonly AIBrainGraph _aiBrainGraph;
        private readonly GameObject _gameObject;
        private Dictionary<AIDecisionNode, AIDecision> _decisions;
        private Dictionary<AIActionNode, AIAction> _actions;

        public GeneratorUtils(AIBrainGraph graph, GameObject go)
        {
            _aiBrainGraph = graph;
            _gameObject = go;
        }
        
        /// <summary>
        /// Generates the <see cref="MoreMountains.Tools.AIBrain"/> system components (Brain, Actions and Decisions)
        /// as defined in the brain graph asset.
        /// </summary>
        public void Generate(bool brainActive, float actionsFrequency, float decisionFrequency, bool generateDebugBrain = false)
        {
            // Removes all Corgi Brain, Action and Decision components
            Cleanup(_gameObject);

            _decisions = new Dictionary<AIDecisionNode, AIDecision>();
            _actions = new Dictionary<AIActionNode, AIAction>();
            
            // Starts the generation process
            GenerateActions(_aiBrainGraph);
            GenerateDecisions(_aiBrainGraph);
            GenerateSubGraphs(_aiBrainGraph);
            GenerateBrain(brainActive, actionsFrequency, decisionFrequency, generateDebugBrain);
        }

        /// <summary>
        /// Generates the <see cref="MoreMountains.Tools.AIBrain"/> system components (Brain, Actions and Decisions)
        /// as defined in the brain graph asset. This method is used at runtime.
        /// </summary>
        /// <param name="brain"></param>
        public void GeneratePluggable(AIBrain brain)
        {
            // Removes all Corgi Brain, Action and Decision components
            Cleanup(_gameObject, true);

            _decisions = new Dictionary<AIDecisionNode, AIDecision>();
            _actions = new Dictionary<AIActionNode, AIAction>();
            
            // Starts the generation process
            GenerateActions(_aiBrainGraph);
            GenerateDecisions(_aiBrainGraph);
            GenerateSubGraphs(_aiBrainGraph);
            InitBrain(brain);
        }

        /// <summary>
        /// Generates the <see cref="MoreMountains.Tools.AIBrain"/> system components from a subgraph.
        /// </summary>
        /// <param name="graph"></param>
        private void GenerateSubGraphs(NodeGraph graph)
        {
            foreach (var subgraphNode in graph.nodes.OfType<AIBrainSubgraphNode>()
                .Select(node => (node)))
            {
                GenerateActions(subgraphNode.subgraph);
                GenerateDecisions(subgraphNode.subgraph);
            }
        }

        /// <summary>
        /// Generates all <see cref="MoreMountains.Tools.AIDecision"/> components attaching them to the gameObject.
        /// </summary>
        private void GenerateDecisions(NodeGraph graph)
        {
            foreach (var decisionNode in graph.nodes.OfType<AIDecisionNode>()
                .Select(node => (node)))
            {
                var aiDecision =  decisionNode.AddDecisionComponent(_gameObject);
                _decisions.Add(decisionNode, aiDecision);
            }
        }

        /// <summary>
        /// Generates all <see cref="MoreMountains.Tools.AIAction"/> components attaching them to the gameObject.
        /// </summary>
        private void GenerateActions(NodeGraph graph)
        {
            foreach (var actionNode in graph.nodes.OfType<AIActionNode>()
                .Select(node => (node)))
            {
                var aiAction =  actionNode.AddActionComponent(_gameObject);
                _actions.Add(actionNode, aiAction);
            }
        }

        /// <summary>
        /// Generates the <see cref="MoreMountains.Tools.AIBrain"/> component creating all
        /// corresponding logic.
        /// </summary>
        private void GenerateBrain(bool brainActive, float actionsFrequency, float decisionFrequency, bool generateDebugBrain)
        {
            // Create the brain
            var brain = generateDebugBrain ? _gameObject.AddComponent<AIBrainDebuggable>() : _gameObject.AddComponent<AIBrain>();
            brain.BrainActive = brainActive;
            brain.ActionsFrequency = actionsFrequency;
            brain.DecisionFrequency = decisionFrequency;
            
            InitBrain(brain);
        }
        
        /// <summary>
        /// Initializes the <see cref="MoreMountains.Tools.AIBrain"/> system wit all required connections.
        /// </summary>
        /// <param name="brain"></param>
        private void InitBrain(AIBrain brain)
        {
            brain.States = new List<AIState>();
            var stateNames = new List<string>();

            #region --- MAIN GRAPH ---

            // Looks for all 'AnyState' transitions
            var globalAnyStateTransitions = new List<AITransition>();
            foreach (var anyStateNode in _aiBrainGraph.nodes.OfType<AIBrainAnyStateNode>())
            {
                var transitionsPort = anyStateNode.GetOutputPort(C.PORT_TRANSITIONS);
                foreach (var transitionNode in transitionsPort.GetConnections().Select(connection => connection.node).OfType<AITransitionNode>())
                {
                    _decisions.TryGetValue(transitionNode.GetDecision(), out var decisionComponent);
                    var trueStateLabel = transitionNode.GetTrueStateLabel();
                    var falseStateLabel = transitionNode.GetFalseStateLabel();
                    if (string.IsNullOrEmpty(trueStateLabel) && string.IsNullOrEmpty(falseStateLabel)) continue;
                    var transition = new AITransition
                    {
                        Decision = decisionComponent,
                        TrueState = trueStateLabel,
                        FalseState = falseStateLabel
                    };
                    globalAnyStateTransitions.Add(transition);
                }
            }
            // Get all states and initialize them
            foreach (var brainStateNode in _aiBrainGraph.nodes.OfType<AIBrainStateNode>()
                .Select(node => node))
            {
                if (stateNames.IndexOf(brainStateNode.name) >= 0)
                {
                    Debug.LogError(C.ERROR_DUPLICATE_STATE_NAMES);
                    return;
                }
                stateNames.Add(brainStateNode.name);
                var aiState = new AIState
                {
                    StateName = brainStateNode.name,
                    Transitions = new AITransitionsList(),
                    Actions = new AIActionsList()
                };
                if (brainStateNode.graph is IBrainGraph graph && graph.StartingNode == brainStateNode)
                {
                    brain.States.Insert(0, aiState);                    
                }
                else
                {
                    brain.States.Add(aiState);
                }

                // Sets all decisions logic
                var transitionsPort = brainStateNode.GetOutputPort(C.PORT_TRANSITIONS);
                foreach (var transitionNode in transitionsPort.GetConnections().Select(connection => connection.node).OfType<AITransitionNode>())
                {
                    _decisions.TryGetValue(transitionNode.GetDecision(), out var decisionComponent);
                    var trueStateLabel = transitionNode.GetTrueStateLabel();
                    var falseStateLabel = transitionNode.GetFalseStateLabel();
                    if (string.IsNullOrEmpty(trueStateLabel) && string.IsNullOrEmpty(falseStateLabel)) continue;
                    var transition = new AITransition
                    {
                        Decision = decisionComponent,
                        TrueState = trueStateLabel,
                        FalseState = falseStateLabel
                    };
                    aiState.Transitions.Add(transition);
                }

                // Adds all global AnyState transitions
                foreach (var transition in globalAnyStateTransitions)
                {
                    aiState.Transitions.Add(transition);
                }

                // Sets all actions logic
                var actionPort = brainStateNode.GetInputPort(C.PORT_ACTIONS);
                foreach (var actionNode in actionPort.GetConnections().Select(connection => connection.node).OfType<AIActionNode>())
                {
                    _actions.TryGetValue(actionNode, out var actionComponent);
                    aiState.Actions.Add(actionComponent);
                }
                
            }
            #endregion

            #region --- SUBGRAPH ---

            foreach (var subgraphNode in _aiBrainGraph.nodes.OfType<AIBrainSubgraphNode>()
                .Select(node => node))
            {
                // Looks for all subgraph 'AnyState' transitions
                var localAnyStateTransitions = new List<AITransition>();
                foreach (var anyStateNode in subgraphNode.subgraph.nodes.OfType<AIBrainAnyStateNode>())
                {
                    var transitionsPort = anyStateNode.GetOutputPort(C.PORT_TRANSITIONS);
                    foreach (var transitionNode in transitionsPort.GetConnections().Select(connection => connection.node).OfType<AITransitionNode>())
                    {
                        _decisions.TryGetValue(transitionNode.GetDecision(), out var decisionComponent);
                        var trueStateLabel = GetSubgraphStateName(subgraphNode.name, transitionNode.GetTrueStateLabel());
                        var falseStateLabel = GetSubgraphStateName(subgraphNode.name, transitionNode.GetFalseStateLabel());
                        if (string.IsNullOrEmpty(trueStateLabel) && string.IsNullOrEmpty(falseStateLabel)) continue;
                        var transition = new AITransition
                        {
                            Decision = decisionComponent,
                            TrueState = trueStateLabel,
                            FalseState = falseStateLabel 
                        };
                        localAnyStateTransitions.Add(transition);
                    }
                }
                // Sets all brain states
                foreach (var brainStateNode in subgraphNode.subgraph.nodes.OfType<AIBrainStateNode>()
                    .Select(node => node))
                {
                    var subName = subgraphNode.name + ">" + brainStateNode.name;
                    if (stateNames.IndexOf(subName) >= 0)
                    {
                        Debug.LogError(C.ERROR_DUPLICATE_STATE_NAMES);
                        return;
                    }
                    stateNames.Add(subName);
                    var aiState = new AIState
                    {
                        StateName = subName,
                        Transitions = new AITransitionsList(),
                        Actions = new AIActionsList()
                    };
                    if (brainStateNode.graph is IBrainGraph graph && graph.StartingNode == brainStateNode && subgraphNode.ParentGraph.StartingNode == subgraphNode)
                    {
                        brain.States.Insert(0, aiState);                    
                    }
                    else
                    {
                        brain.States.Add(aiState);
                    }

                    // Sets all decisions logic
                    var transitionsPort = brainStateNode.GetOutputPort(C.PORT_TRANSITIONS);
                    foreach (var transitionNode in transitionsPort.GetConnections().Select(connection => connection.node).OfType<AITransitionNode>())
                    {
                        _decisions.TryGetValue(transitionNode.GetDecision(), out var decisionComponent);
                        var trueStateLabel = GetSubgraphStateName(subgraphNode.name, transitionNode.GetTrueStateLabel());
                        var falseStateLabel = GetSubgraphStateName(subgraphNode.name, transitionNode.GetFalseStateLabel());
                        if (string.IsNullOrEmpty(trueStateLabel) && string.IsNullOrEmpty(falseStateLabel)) continue;
                        var transition = new AITransition
                        {
                            Decision = decisionComponent,
                            TrueState = trueStateLabel,
                            FalseState = falseStateLabel
                        };
                        aiState.Transitions.Add(transition);
                    }
                    // Adds all global AnyState transitions
                    foreach (var transition in globalAnyStateTransitions)
                    {
                        aiState.Transitions.Add(transition);
                    }
                    // Adds all local AnyState transitions
                    foreach (var transition in localAnyStateTransitions)
                    {
                        aiState.Transitions.Add(transition);
                    }

                    // Sets all actions logic
                    var actionPort = brainStateNode.GetInputPort(C.PORT_ACTIONS);
                    foreach (var actionNode in actionPort.GetConnections().Select(connection => connection.node).OfType<AIActionNode>())
                    {
                        _actions.TryGetValue(actionNode, out var actionComponent);
                        aiState.Actions.Add(actionComponent);
                    }
                }
                
                
                foreach (var transitionsPort in subgraphNode.DynamicOutputs)
                {
                    var stateName = GetSubgraphStateName(subgraphNode.name, transitionsPort.fieldName.Split('-')[0]);
                    foreach (var transitionNode in transitionsPort.GetConnections().Select(connection => connection.node).OfType<AITransitionNode>())
                    {
                        _decisions.TryGetValue(transitionNode.GetDecision(), out var decisionComponent);
                        var trueStateLabel = string.IsNullOrEmpty(transitionNode.GetTrueStateLabel()) ? "" : transitionNode.GetTrueStateLabel();
                        var falseStateLabel = string.IsNullOrEmpty(transitionNode.GetFalseStateLabel()) ? "" : transitionNode.GetFalseStateLabel();
                        if (string.IsNullOrEmpty(trueStateLabel) && string.IsNullOrEmpty(falseStateLabel)) continue;
                        var transition = new AITransition
                        {
                            Decision = decisionComponent,
                            TrueState = trueStateLabel,
                            FalseState = falseStateLabel
                        };
                        foreach (var state in brain.States.Where(state => state.StateName == stateName))
                        {
                            state.Transitions.Add(transition);
                        }
                    }
                }

            }

            #endregion
        }

        #region --- MASTER/SLAVE ---

        /// <summary>
        /// Adds a Slave brain to a Character.
        /// </summary>
        /// <param name="channelNames"></param>
        /// <param name="gameObject"></param>
        public static void AddSlaveBrain(List<string> channelNames, GameObject gameObject)
        {
            var brainSlave = gameObject.AddComponent<BrainSlave>();
            brainSlave.ChannelNames = channelNames;
        }

        /// <summary>
        /// Removes the Slave brain component from the character.
        /// </summary>
        /// <param name="gameObject"></param>
        public static void RemoveSlaveBrain(GameObject gameObject)
        {
            var brainSlave = gameObject.GetComponent<BrainSlave>();
            if (brainSlave == null) return;
            Object.DestroyImmediate(brainSlave);
        }

        #endregion

        /// <summary>
        /// Removes all Corgi Brain, Actions and Decisions from the gameObject.
        /// </summary>
        public static void Cleanup(GameObject go, bool excludeBrain = false)
        {
            if (!excludeBrain)
            {
                var brain = go.GetComponent<AIBrain>();
                Object.DestroyImmediate(brain);
            }

            var remainingActions = false;
            var remainingDecisions = false;

            // Loops twice if there is a required component for the destroying component itself
            var count = 0;
            while (count < 2)
            {

                foreach (var aiDecision in go.GetComponents<AIDecision>())
                {
                    if (go.CanDestroyAIDecision(aiDecision.GetType()))
                    {
                        Object.DestroyImmediate(aiDecision);
                    }
                    else
                    {
                        remainingDecisions = true;
                    }
                }

                foreach (var aiAction in go.GetComponents<AIAction>())
                {
                    if (go.CanDestroyAIAction(aiAction.GetType()))
                    {
                        Object.DestroyImmediate(aiAction);
                    }
                    else
                    {
                        remainingActions = true;
                    }
                }

                if (remainingActions || remainingDecisions)
                {
                    count++;
                    continue;
                }
                break;
            }
        }
    }
}