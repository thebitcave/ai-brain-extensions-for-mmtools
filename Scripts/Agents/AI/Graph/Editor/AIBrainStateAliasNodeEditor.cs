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
            _node.name = !string.IsNullOrEmpty(_node.stateName) ? _node.stateName : C.LABEL_STATE_ALIAS;
            base.OnHeaderGUI();
        }
        
        public override void OnBodyGUI()
        {
            _statesIn = serializedObject.FindProperty("statesIn");

            if (Selection.activeObject == _node)
            {
                var optionsList = new List<string>();
                foreach (var node in _node.graph.nodes.OfType<AIBrainStateNode>())
                {
                    optionsList.Add(node.name);
                }
                foreach (var node in _node.graph.nodes.OfType<AIBrainSubgraphNode>())
                {
                    foreach (var stateName in node.inputStates.Select(inputState => GeneratorUtils.GetSubgraphStateName(node.name, inputState.fieldName)))
                    {
                        optionsList.Add(stateName);
                    }
                }

                if (optionsList.Count > 0)
                {
                    var options = optionsList.ToArray();
                    _stateIndex = EditorGUILayout.Popup(_stateIndex, options);
                    EditorGUILayout.Space();
                    _node.stateName = options[_stateIndex];
                }
                else
                {
                    EditorGUILayout.LabelField(C.LABEL_NO_STATE_AVAILABLE);
                    EditorGUILayout.Space();
                }
            }

            serializedObject.Update();
            NodeEditorGUILayout.PropertyField(_statesIn);
            serializedObject.ApplyModifiedProperties();
        }
        
        /// <summary> Add items for the context menu when right-clicking this node. Disables the 'Rename' option. </summary>
        public override void AddContextMenuItems(GenericMenu menu)
        {
            // Actions if only one node is selected
            if (Selection.objects.Length == 1 && Selection.activeObject is XNode.Node)
            {
                var node = Selection.activeObject as XNode.Node;
                menu.AddItem(new GUIContent("Move To Top"), false, () => NodeEditorWindow.current.MoveNodeToTop(node));
                menu.AddDisabledItem(new GUIContent("Rename"));
            }

            // Add actions to any number of selected nodes
            menu.AddItem(new GUIContent("Copy"), false, NodeEditorWindow.current.CopySelectedNodes);
            menu.AddItem(new GUIContent("Duplicate"), false, NodeEditorWindow.current.DuplicateSelectedNodes);
            menu.AddItem(new GUIContent("Remove"), false, NodeEditorWindow.current.RemoveSelectedNodes);

            // Custom sections if only one node is selected
            if (Selection.objects.Length == 1 && Selection.activeObject is XNode.Node)
            {
                var node = Selection.activeObject as XNode.Node;
                menu.AddCustomContextMenuItems(node);
            }
        }

    }
}