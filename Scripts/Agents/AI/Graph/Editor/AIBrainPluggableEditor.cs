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
        protected ReorderableList _brainList;

        protected AIBrainPluggable _pluggableBrain;
        
        protected override void OnEnable()
        {
            base.OnEnable();
            
            _pluggableBrain = target as AIBrainPluggable;

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
            EditorGUILayout.Space();
            _brainList.DoLayoutList();
            var graphName = string.IsNullOrEmpty(_pluggableBrain.GraphName) ? "-" : _pluggableBrain.GraphName;
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.LabelField(C.LABEL_SELECTED_GRAPH + ": " + _pluggableBrain.GraphName);
            EditorGUI.EndDisabledGroup();
            serializedObject.ApplyModifiedProperties();
        }

    }
}
