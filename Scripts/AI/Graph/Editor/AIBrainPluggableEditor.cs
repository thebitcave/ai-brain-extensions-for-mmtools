using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;
using UnityEditor;

namespace TheBitCave.MMToolsExtensions.AI.Graph
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(AIBrainPluggable))]
    public class AIBrainPluggableEditor : AIBrainEditor
    {
        protected SerializedProperty _generateSlaveBrain;
        protected SerializedProperty _slaveChannelName;

        protected ReorderableList _brainList;
        
        protected override void OnEnable()
        {
            base.OnEnable();

            _generateSlaveBrain = serializedObject.FindProperty("generateSlaveBrain");
            _slaveChannelName = serializedObject.FindProperty("slaveChannelName");

            _brainList = new ReorderableList(serializedObject.FindProperty("aiBrainGraphs"))
            {
                elementNameProperty = "aiBrainGraphs",
                elementDisplayType = ReorderableList.ElementDisplayType.Expandable,
            };
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            serializedObject.Update();
            EditorGUILayout.PropertyField(_generateSlaveBrain);
            if(_generateSlaveBrain.boolValue) EditorGUILayout.PropertyField(_slaveChannelName);
            EditorGUILayout.Space();
            _brainList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }

    }
}
