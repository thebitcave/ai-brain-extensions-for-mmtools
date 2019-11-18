using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TheBitCave.MMToolsExtensions.AI.Graph
{

    public class AIBrainDebuggerEditor : EditorWindow
    {
        [MenuItem("Tools/TheBitCave/MM Extensions/AI Brain Debugger")]
        private static void Init()
        {
            var window = GetWindow<AIBrainDebuggerEditor>("AI Brain Debugger", true);
            window.Show();
        }

        private void Awake()
        {
            throw new NotImplementedException();
        }

        private void Update()
        {
            throw new NotImplementedException();
        }

        private void OnInspectorUpdate()
        {
            throw new NotImplementedException();
        }

        private void OnGUI()
        {
            throw new NotImplementedException();
        }
    }
}