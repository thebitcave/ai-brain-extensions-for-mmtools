using System;
using MoreMountains.Tools;
using UnityEditor;
using UnityEngine;

namespace TheBitCave.MMToolsExtensions.AI.Graph
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

        [MenuItem("Tools/TheBitCave/MM Extensions/AI Brain Debugger")]
        private static void Init()
        {
            var window = GetWindow<AIBrainDebuggerEditorWindow>("AI Brain Debugger", true);
            window.Show();
        }

     //   private void Awake()
     //   {
            // throw new NotImplementedException();
     //   }

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
            var titleStyle = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleCenter,
                fontStyle = FontStyle.Bold,
                fontSize = 11
            };
            
            var labelStyle = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleCenter
            };
            
            if (_selectedBrain == null)
            {
                EditorGUI.LabelField(new Rect(0, 0, position.width, position.height), C.AIBRAIN_DEBUGGER_NO_AIBRAIN_COMPONENT, labelStyle);
            }
            else if (!Application.isPlaying)
            {
                EditorGUI.LabelField(new Rect(0, 0, position.width, position.height), C.AIBRAIN_DEBUGGER_APPLICATION_NOT_PLAYING, labelStyle);
            }
            else if (!_selectedBrain.gameObject.activeInHierarchy)
            {
                EditorGUI.LabelField(new Rect(0, 0, position.width, position.height), C.AIBRAIN_DEBUGGER_GAMEOBJECT_DISABLED, labelStyle);
            }
            else
            {
                EditorGUILayout.BeginVertical();
                _scrollPos =
                    EditorGUILayout.BeginScrollView(_scrollPos, GUILayout.ExpandWidth(true));
                EditorGUILayout.LabelField(C.AIBRAIN_DEBUGGER_SELECTED_BRAIN_LABEL + _selectedGameObject.name, titleStyle, null);

                var activeStatus = _selectedBrain.BrainActive
                    ? C.AIBRAIN_DEBUGGER_ACTIVE_LABEL
                    : C.AIBRAIN_DEBUGGER_INACTIVE_LABEL;
                EditorGUILayout.LabelField(C.AIBRAIN_DEBUGGER_BRAIN_IS_LABEL + activeStatus, labelStyle, null);

                _previousStateName = _currentStateName == _selectedBrain.CurrentState.StateName
                    ? _previousStateName
                    : _currentStateName;
                _currentStateName = _selectedBrain.CurrentState.StateName;
                EditorGUILayout.LabelField("Current State: " + _selectedBrain.CurrentState.StateName, labelStyle, null);
                EditorGUILayout.LabelField("Time in this state: " + _selectedBrain.TimeInThisState, labelStyle, null);

                var text = "";
                foreach (var action in _actionList)
                {
                    if (action == null) continue;
                    text += action.GetType().Name;
                    text += ", ";
                }

                EditorGUILayout.LabelField("Performing: " + text, labelStyle, null);
                EditorGUILayout.LabelField("Target: " + _selectedBrain.Target, labelStyle, null);
                
                EditorGUILayout.LabelField("------------------------------", labelStyle, null);
                EditorGUILayout.LabelField("Previous State: " + _previousStateName, labelStyle, null);
                EditorGUILayout.LabelField("Time in the previous state: " + _selectedBrain.TimeInPreviousState, labelStyle, null);
                
                EditorGUILayout.BeginHorizontal();
                foreach (var aiState in _selectedBrain.States)
                {
                    var style = new GUIStyle(GUI.skin.button) {normal = {textColor = Color.black}};
                    foreach (var transition in _selectedBrain.CurrentState.Transitions)
                    {
                        if (transition.FalseState == aiState.StateName || transition.TrueState == aiState.StateName)
                        {
                            GUI.backgroundColor = (transition.FalseState == aiState.StateName || transition.TrueState == aiState.StateName) ?
                                new Color(.9f, .3f, .9f, 1):
                                Color.white;
                        }
                    }

                    EditorGUI.BeginDisabledGroup(_selectedBrain.CurrentState.StateName == aiState.StateName);
                    if(GUILayout.Button(aiState.StateName, style)) TransitionToState(aiState.StateName);
                    EditorGUI.EndDisabledGroup();
                }
                GUI.backgroundColor = Color.white;
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.LabelField("------------------------------", labelStyle, null);

                aiBrainTarget = EditorGUILayout.ObjectField("Target", aiBrainTarget, typeof(GameObject), true) as GameObject;
                EditorGUI.BeginDisabledGroup(aiBrainTarget == null || !aiBrainTarget.scene.IsValid());
                if(GUILayout.Button("Set Target")) _selectedBrain.Target = aiBrainTarget.transform;
                EditorGUI.EndDisabledGroup();
                EditorGUI.BeginDisabledGroup(aiBrainTarget == null);
                if (GUILayout.Button("Remove Target"))
                {
                    _selectedBrain.Target = null;
                    aiBrainTarget = null;
                }
                EditorGUI.EndDisabledGroup();
                
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