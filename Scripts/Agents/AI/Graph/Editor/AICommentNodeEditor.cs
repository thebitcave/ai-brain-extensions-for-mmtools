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
            const string title = C.LABEL_COMMENT;
            GUILayout.Label(title, NodeEditorResources.styles.nodeHeader, GUILayout.Height(30));
        }

        public override void OnBodyGUI()
        {
            var wordWrap = EditorStyles.textField.wordWrap;
            EditorStyles.textField.wordWrap = true;

            if (Selection.activeObject == _node)
            {
                _node.comment = EditorGUILayout.TextArea(_node.comment);
            }
            else
            {
                EditorGUI.BeginDisabledGroup(true);
                GUIStyle style = GUIStyle.none;
                style.wordWrap = true;
                EditorGUILayout.TextArea(_node.comment, GUIStyle.none);
                EditorGUI.EndDisabledGroup();
            }

            EditorStyles.textField.wordWrap = wordWrap;
        }
    }
}