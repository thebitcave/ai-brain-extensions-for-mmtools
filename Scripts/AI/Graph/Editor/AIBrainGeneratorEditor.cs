using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TheBitCave.MMToolsExtensions.AI.Graph
{
    [CustomEditor (typeof(AIBrainGenerator))]
    public class AIBrainGeneratorEditor : Editor
    {
        
        protected SerializedProperty _aiBrainGraph;

        protected SerializedProperty _brainActive;
        protected SerializedProperty _actionsFrequency;
        protected SerializedProperty _decisionFrequency;
        protected SerializedProperty _generateDebugBrain;
        protected SerializedProperty _generateSlaveBrain;
        protected SerializedProperty _slaveChannelName;

        private AIBrainGenerator _generator;
        
        private void OnEnable()
        {
            _generator = target as AIBrainGenerator;

            _aiBrainGraph = serializedObject.FindProperty("aiBrainGraph");
            
            _brainActive = serializedObject.FindProperty("brainActive");
            _actionsFrequency = serializedObject.FindProperty("actionsFrequency");
            _decisionFrequency = serializedObject.FindProperty("decisionFrequency");
            _generateDebugBrain = serializedObject.FindProperty("generateDebugBrain");
            _generateSlaveBrain = serializedObject.FindProperty("generateSlaveBrain");
            _slaveChannelName = serializedObject.FindProperty("slaveChannelName");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(_aiBrainGraph);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(_brainActive);
            EditorGUILayout.PropertyField(_actionsFrequency);
            EditorGUILayout.PropertyField(_decisionFrequency);
            EditorGUILayout.PropertyField(_generateDebugBrain);
            EditorGUILayout.PropertyField(_generateSlaveBrain);
            if(_generateSlaveBrain.boolValue) EditorGUILayout.PropertyField(_slaveChannelName);
            serializedObject.ApplyModifiedProperties();

            EditorGUILayout.Space();

            if(GUILayout.Button(C.LABEL_REMOVE_AI_SCRIPTS))
            {
                _generator.Cleanup();
            }

            if(GUILayout.Button(C.LABEL_GENERATE))
            {
                _generator.Generate();
            }

            EditorGUILayout.HelpBox(C.WARNING_GENERATE_SCRIPTS, MessageType.Warning);
        }
    }
}