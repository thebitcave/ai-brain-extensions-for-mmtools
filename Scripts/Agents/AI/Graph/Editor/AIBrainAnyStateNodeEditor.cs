using UnityEngine;
using XNodeEditor;

namespace TheBitCave.MMToolsExtensions.AI.Graph
{
    [CustomNodeEditor(typeof(AIBrainAnyStateNode))]
    public class AIBrainAnyStateNodeEditor : NodeEditor
    {
        public override void OnHeaderGUI()
        {
            GUILayout.Label(C.LABEL_ANY_STATE, NodeEditorResources.styles.nodeHeader, GUILayout.Height(30));
        }
    }
}