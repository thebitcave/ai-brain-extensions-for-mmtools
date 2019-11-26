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
        
        protected override void OnEnable()
        {
            base.OnEnable();

            _brainList = new ReorderableList(serializedObject.FindProperty("aiBrainGraphs"))
            {
                elementNameProperty = "aiBrainGraphs",
                elementDisplayType = ReorderableList.ElementDisplayType.Expandable,
            };
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
//            serializedObject.Update();
            _brainList.DoLayoutList();
 //           serializedObject.ApplyModifiedProperties();
        }

    }
}
