using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace TheBitCave.MMToolsExtensions.AI.Graph
{
    [CustomNodeEditor(typeof(AICommentNode))]
    public class AICommentNodeEditor : NodeEditor
    {
        protected AICommentNode _node;

        public override void OnCreate()
        {
            base.OnCreate();
            _node = target as AICommentNode;
        }

        public override void OnHeaderGUI()
        {
            GUILayout.Label(C.LABEL_COMMENT, NodeEditorResources.styles.nodeHeader, GUILayout.Height(30));
        }

        public override void OnBodyGUI()
        {
            var style = GUIStyle.none;
            style.wordWrap = true;

            if (Selection.activeObject == _node)
            {
                _node.comment = EditorGUILayout.TextArea(_node.comment, style);
            }
            else
            {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.TextArea(_node.comment, style);
                EditorGUI.EndDisabledGroup();
            }
        }
    }
}