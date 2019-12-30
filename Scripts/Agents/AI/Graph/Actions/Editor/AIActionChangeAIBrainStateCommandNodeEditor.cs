using UnityEditor;
using XNodeEditor;

namespace TheBitCave.MMToolsExtensions.AI.Graph
{
    [CustomNodeEditor(typeof(AIActionChangeAIBrainStateCommandNode))]
    public class AIActionChangeAIBrainStateCommandNodeEditor : AIActionNodeEditor
    {
        private SerializedProperty _channel;
        private SerializedProperty _stateName;
        
        public override void OnBodyGUI()
        {
            base.OnBodyGUI();
            
            EditorGUIUtility.labelWidth = 100;
            _channel = serializedObject.FindProperty("channel");
            _stateName = serializedObject.FindProperty("stateName");

            serializedObject.Update();
            NodeEditorGUILayout.PropertyField(_channel);
            NodeEditorGUILayout.PropertyField(_stateName);
            serializedObject.ApplyModifiedProperties();
        }
    }
}