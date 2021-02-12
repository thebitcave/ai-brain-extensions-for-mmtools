using System.Linq;
using MoreMountains.Tools;
using TheBitCave.MMToolsExtensions.AI.Graph;
using UnityEditor;
using UnityEngine;

namespace TheBitCave.MMToolsExtensions.AI
{
    public class AIBrainDebuggerEditorWindow : EditorWindow
    {
    
        /// <summary>
        /// The gameobject containing the AIBrain that should be debugged.
        /// </summary>
        public GameObject aiBrainTarget;
        
        private GameObject _selectedGameObject;
        private AIBrainDebuggable _selectedBrain;
        
        private AIActionsList _actionList;
        
        private string _currentStateName;
        private string _previousStateName;

        private Vector2 _scrollPos; // Used by the scroll window

        [MenuItem("Tools/The Bit Cave/MM Extensions/AI Brain Debugger")]
        private static void Init()
        {
            var window = GetWindow<AIBrainDebuggerEditorWindow>("AI Brain Debugger", true);
            window.Show();
        }

        private void Update()
        {
            if (Selection.activeObject == _selectedGameObject && _selectedBrain != null) return;
            if (Selection.activeGameObject == null) return;

            if (_selectedBrain != null)
                _selectedBrain.onPerformingActions -= OnBrainPerformingActions;
            _selectedGameObject = Selection.activeGameObject;
            _selectedBrain = _selectedGameObject.GetComponent<AIBrainDebuggable>();
            if (_selectedBrain != null)
                _selectedBrain.onPerformingActions += OnBrainPerformingActions;
            aiBrainTarget = null;
        }

        private void OnInspectorUpdate()
        {
            Repaint();
        }

        private void OnGUI()
        {
            #region --- STYLES ---

            var mainTitleStyle = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleCenter,
                fontStyle = FontStyle.Bold,
                fontSize = 12
            };

            var titleStyle = new GUIStyle(GUI.skin.label)
            {
                fontStyle = FontStyle.Bold,
                fontSize = 11
            };

            var titleStyleCenter = new GUIStyle(GUI.skin.label)
            {
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                fontSize = 11
            };

            var labelStyle = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleCenter
            };

            var targetButtonStyle = new GUIStyle(GUI.skin.button)
            {
                fixedWidth = 100
            };
            
            #endregion
            
            if (_selectedBrain == null)
            {
                EditorGUILayout.Space();
                EditorGUILayout.HelpBox(C.DEBUG_NO_AIBRAIN_COMPONENT, MessageType.Info);
            }
            else if (!Application.isPlaying)
            {
                EditorGUILayout.Space();
                EditorGUILayout.HelpBox(C.DEBUG_APPLICATION_NOT_PLAYING, MessageType.Warning);
            }
            else if (!_selectedBrain.gameObject.activeInHierarchy)
            {
                EditorGUILayout.Space();
                EditorGUILayout.HelpBox(C.DEBUG_GAMEOBJECT_DISABLED, MessageType.Warning);
            }
            else
            {
                EditorGUILayout.BeginVertical();
                
                _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, GUILayout.ExpandWidth(true));

                #region --- HEADER ---

                EditorGUILayout.Space();
                EditorGUILayout.LabelField(C.DEBUG_SELECTED_BRAIN_LABEL + _selectedGameObject.name, mainTitleStyle, null);

                var label = C.DEBUG_BRAIN_IS_LABEL;
                label += _selectedBrain.BrainActive
                    ? C.DEBUG_ACTIVE_LABEL
                    : C.DEBUG_INACTIVE_LABEL;

                label += " | ";
                label += _selectedBrain.Target == null ? C.DEBUG_TARGET_NULL_LABEL : C.DEBUG_TARGET_LABEL + ": " + _selectedBrain.Target.name;
                EditorGUILayout.LabelField(label, labelStyle, null);

                #endregion

                #region --- STATE TRACKING --

                _previousStateName = _currentStateName == _selectedBrain.CurrentState.StateName
                    ? _previousStateName
                    : _currentStateName;
                _currentStateName = _selectedBrain.CurrentState.StateName;

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.BeginVertical(GUI.skin.box);

                label = C.DEBUG_PREVIOUS_STATE_LABEL;
                EditorGUILayout.LabelField(label, titleStyleCenter, null);
                label = _previousStateName;
                EditorGUILayout.LabelField(label, labelStyle, null);
                label = C.DEBUG_TIME_IN_STATE_LABEL + ": " + _selectedBrain.TimeInPreviousState.ToString("0.##");
                EditorGUILayout.LabelField(label, labelStyle, null);

                EditorGUILayout.EndVertical();
                EditorGUILayout.BeginVertical(GUI.skin.box); 
                
                label = C.DEBUG_CURRENT_STATE_LABEL;
                EditorGUILayout.LabelField(label, titleStyleCenter, null);
                label = _currentStateName;
                EditorGUILayout.LabelField(label, labelStyle, null);
                label = C.DEBUG_TIME_IN_STATE_LABEL + ": " + _selectedBrain.TimeInThisState.ToString("0.##");
                EditorGUILayout.LabelField(label , labelStyle, null);
                
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndHorizontal();

                #endregion


                #region --- ACTIONS ---

                var ar = (from action in _actionList where action != null select action.GetType().Name).ToArray();
                label = C.DEBUG_PERFORMING_LABEL + ": " + string.Join(", ", ar);

                EditorGUILayout.LabelField(label, labelStyle, null);
                
                #endregion

                EditorGUILayout.Space();

                #region --- STATE TRANSITIONS ---

                label = C.DEBUG_STATE_TRANSITIONS_LABEL;
                EditorGUILayout.LabelField(label, titleStyle);

                foreach (var aiState in _selectedBrain.States)
                {
                    var buttonLabel = aiState.StateName;
                    GUI.backgroundColor = Color.white;
                    if (_selectedBrain.CurrentState.StateName == aiState.StateName)
                    {
                        GUI.backgroundColor = new Color(0f, .8f, 1f, 1);
                        buttonLabel = "[C] " + aiState.StateName;
                    }
                    foreach (var transition in _selectedBrain.CurrentState.Transitions)
                    {
                        if (transition.FalseState != aiState.StateName && transition.TrueState != aiState.StateName)
                            continue;
                        GUI.backgroundColor = new Color(0f, .7f, 1f, 1);
                        buttonLabel = "[>>] " + aiState.StateName;
                    }

                    EditorGUI.BeginDisabledGroup(_selectedBrain.CurrentState.StateName == aiState.StateName);
                    if(GUILayout.Button(buttonLabel)) TransitionToState(aiState.StateName);
                    EditorGUI.EndDisabledGroup();
                }
                GUI.backgroundColor = Color.white;
                
                #endregion

                EditorGUILayout.Space();

                #region --- TARGET ---
                
                label = C.DEBUG_SET_TARGET_LABEL;
                EditorGUILayout.LabelField(label, titleStyle);

                EditorGUILayout.BeginHorizontal();
                aiBrainTarget = EditorGUILayout.ObjectField(C.DEBUG_TARGET_LABEL, aiBrainTarget, typeof(GameObject), true) as GameObject;
                EditorGUI.BeginDisabledGroup(aiBrainTarget == null || !aiBrainTarget.scene.IsValid());
                if(GUILayout.Button(C.DEBUG_SET_LABEL, targetButtonStyle) && aiBrainTarget != null) _selectedBrain.Target = aiBrainTarget.transform;
                EditorGUI.EndDisabledGroup();
                EditorGUI.BeginDisabledGroup(aiBrainTarget == null);
                if (GUILayout.Button(C.DEBUG_REMOVE_LABEL, targetButtonStyle))
                {
                    _selectedBrain.Target = null;
                    aiBrainTarget = null;
                }
                EditorGUI.EndDisabledGroup();
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.LabelField("Target: " + _selectedBrain.Target);
                EditorGUILayout.LabelField("LastKnownTargetPosition: " + _selectedBrain._lastKnownTargetPosition);

                #endregion
                
                EditorGUILayout.EndScrollView();
                EditorGUILayout.EndVertical();
            }
        }

        private void TransitionToState(string stateName)
        {
            _selectedBrain.TransitionToState(stateName);
        }
        
        private void OnBrainPerformingActions(AIActionsList actionList)
        {
            _actionList = actionList;
        }

        private void OnDisable()
        {
            aiBrainTarget = null;
        }
    }
}
