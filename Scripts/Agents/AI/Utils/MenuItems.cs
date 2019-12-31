using MoreMountains.Tools;
using UnityEngine;
using UnityEditor;

namespace TheBitCave.MMToolsExtensions.AI.Graph
{
    public static class MenuItems
    {
        
        [MenuItem("GameObject/AI Brain/Remove Brain System", true, 30)]
        private static bool RemoveAIBrainSystemValidator()
        {
            if (Selection.activeGameObject == null) return false;
            var aiBrain = Selection.activeGameObject.GetComponent<AIBrain>();
            var aiActions = Selection.activeGameObject.GetComponents<AIAction>();
            var aiDecisions = Selection.activeGameObject.GetComponents<AIDecision>();

            return (aiBrain != null) || (aiActions.Length > 0) || (aiDecisions.Length > 0);
        }

        /// <summary>
        /// Removes all the AI System (AIBrain, AIActions and AIDecisions)
        /// </summary>
        [MenuItem("GameObject/AI Brain/Remove Brain System", false, 30)]
        private static void RemoveAIBrainSystem()
        {
            if (Selection.activeGameObject == null) return;
            var aiBrain = Selection.activeGameObject.GetComponent<AIBrain>();
            var aiActions = Selection.activeGameObject.GetComponents<AIAction>();
            var aiDecisions = Selection.activeGameObject.GetComponents<AIDecision>();

            if(aiBrain) Debug.Log("Removing AIBrain");
            if(aiActions.Length > 0) Debug.Log("Removing " + aiActions.Length + " AIActions");
            if(aiDecisions.Length > 0) Debug.Log("Removing " + aiDecisions.Length + " AIDecisions");

            GeneratorUtils.Cleanup(Selection.activeGameObject);
            
            Debug.Log("AIBrain cleanup complete.");
        }
    }
}
