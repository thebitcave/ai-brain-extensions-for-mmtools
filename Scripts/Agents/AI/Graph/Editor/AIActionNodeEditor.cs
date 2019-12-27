using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace TheBitCave.MMToolsExtensions.AI.Graph
{
    [CustomNodeEditor(typeof(AIActionNode))]
    public class AIActionNodeEditor : NodeEditor
    {
        private SerializedProperty _label;
        private SerializedProperty _output;

        public override void OnHeaderGUI()
        {
            var title = target.name.Replace("AI Action ", "");
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