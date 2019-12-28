using System.Linq;
using Boo.Lang;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace TheBitCave.MMToolsExtensions.AI.Graph
{
    [CustomNodeEditor(typeof(AIBrainStateAliasNode))]
    public class AIBrainStateAliasNodeEditor : NodeEditor
    {
        private SerializedProperty _statesIn;

        protected AIBrainStateAliasNode _node;

        protected int _stateIndex = 0;
        
        public override void OnCreate()
        {
            base.OnCreate();
            _node = target as AIBrainStateAliasNode;
        }

        public override void OnHeaderGUI()
        {
            GUILayout.Label(C.LABEL_STATE_ALIAS, NodeEditorResources.styles.nodeHeader, GUILayout.Height(30));
        }
        
        public override void OnBodyGUI()
        {
            var optionsList = new List<string>();
            foreach (var node in _node.graph.nodes.OfType<AIBrainStateNode>())
            {
                optionsList.Add(node.name);
            }
            var options = optionsList.ToArray();
            _stateIndex = EditorGUILayout.Popup(_stateIndex, options);
            _statesIn = serializedObject.FindProperty("statesIn");

            EditorGUILayout.Space();

            serializedObject.Update();
            NodeEditorGUILayout.PropertyField(_statesIn);
            serializedObject.ApplyModifiedProperties();

            _node.stateName = options[_stateIndex];
        }

    }
}