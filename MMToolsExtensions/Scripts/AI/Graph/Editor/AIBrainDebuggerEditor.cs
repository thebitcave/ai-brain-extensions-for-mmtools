using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEditor;
using UnityEngine;

namespace TheBitCave.MMToolsExtensions.AI.Graph
{

    public class AIBrainDebuggerEditor : EditorWindow
    {
    
        private GameObject _selectedGameObject;
        private AIBrainDebuggable _selectedBrain;
        
        private AIActionsList _actionList;
        
        private string _currentStateName;
        private string _previousStateName;

        [MenuItem("Tools/TheBitCave/MM Extensions/AI Brain Debugger")]
        private static void Init()
        {
            var window = GetWindow<AIBrainDebuggerEditor>("AI Brain Debugger", true);
            window.Show();
        }

        private void Awake()
        {
            // throw new NotImplementedException();
        }

        private void Update()
        {
            if (Selection.activeObject == _selectedGameObject && _selectedBrain != null) return;
            if (Selection.activeGameObject == null) return;

            _selectedBrain.onPerformingActions -= OnBrainPerformingActions;
            _selectedGameObject = Selection.activeGameObject;
            _selectedBrain = _selectedGameObject.GetComponent<AIBrainDebuggable>();
            _selectedBrain.onPerformingActions += OnBrainPerformingActions;
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
                fontStyle = FontStyle.Bold
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
            else
            {
                EditorGUILayout.BeginVertical();
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
                EditorGUILayout.LabelField("" + _selectedBrain.TimeInThisState, labelStyle, null);

                var text = "";
                foreach (var action in _actionList)
                {
                    if (action == null) continue;
                    text += action.GetType().Name;
                    text += ", ";
                }

                EditorGUILayout.LabelField("Performing: " + text, labelStyle, null);
                
                EditorGUILayout.LabelField("------------------------------", labelStyle, null);
                EditorGUILayout.LabelField("Previous State: " + _previousStateName, labelStyle, null);
                EditorGUILayout.LabelField("" + _selectedBrain.TimeInPreviousState, labelStyle, null);

                EditorGUILayout.EndVertical();
            }
        }
        
        private void OnBrainPerformingActions(AIActionsList actionList)
        {
            _actionList = actionList;
        }


    }
}