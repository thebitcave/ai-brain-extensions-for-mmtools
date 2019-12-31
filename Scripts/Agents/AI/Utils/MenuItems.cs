using UnityEngine;
using UnityEditor;

namespace TheBitCave.MMToolsExtensions.AI.Graph
{
    public static class MenuItems
    {
        /// <summary>
        /// Removes all the AI System (AIBrain, AIActions and AIDecisions)
        /// </summary>
        [MenuItem("GameObject/AI Brain/Remove AI Brain System", false, 100)]
        private static void RemoveAIBrainSystem()
        {
            if (Selection.activeGameObject == null) return;
            GeneratorUtils.Cleanup(Selection.activeGameObject);
        }
    }
}
