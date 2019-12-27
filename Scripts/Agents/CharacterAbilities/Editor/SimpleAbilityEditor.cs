using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TheBitCave.MMToolsExtensions
{
    [CustomEditor (typeof(SimpleAbility),true)]
    public class SimpleAbilityEditor : Editor
    {
        protected SerializedProperty _abilityFeedbacks;

        private SimpleAbility _ability;
        private void OnEnable()
        {
            _ability = target as SimpleAbility;
            ;
            
            _abilityFeedbacks = this.serializedObject.FindProperty("AbilityFeedbacks");
        }
        
		public override void OnInspectorGUI()
		{

			serializedObject.Update();
            EditorGUI.BeginChangeCheck();

            if (_ability.HelpBoxText() != "")
			{
				EditorGUILayout.HelpBox(_ability.HelpBoxText(),MessageType.Info);
			}

			Editor.DrawPropertiesExcluding(serializedObject, new string[] { "AbilityFeedbacks" });

			EditorGUILayout.Space();
                        
            EditorGUILayout.LabelField("Feedbacks", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_abilityFeedbacks);

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }                
        }	

    }
}