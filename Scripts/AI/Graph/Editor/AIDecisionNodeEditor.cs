using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace TheBitCave.MMToolsExtensions.AI.Graph
{
    [CustomNodeEditor(typeof(AIDecisionNode))]
    public class AIDecisionNodeEditor : NodeEditor
    {
        private SerializedProperty _label;
        private SerializedProperty _output;
        
        public override void OnHeaderGUI()
        {
            var title = target.name.Replace("AI Decision ", "");
            GUILayout.Label(title, NodeEditorResources.styles.nodeHeader, GUILayout.Height(30));
        }

        public override void OnBodyGUI()
        {
            _label = serializedObject.FindProperty("label");
            _output = serializedObject.FindProperty("output");
            
            serializedObject.Update();
            NodeEditorGUILayout.PropertyField(_label);
            NodeEditorGUILayout.PropertyField(_output);
            serializedObject.ApplyModifiedProperties();
        }

    }
}