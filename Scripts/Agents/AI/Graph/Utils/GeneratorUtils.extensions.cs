using UnityEngine;
using System;
using System.Linq;
using System.Reflection;
using MoreMountains.Tools;

namespace TheBitCave.MMToolsExtensions.AI.Graph
{
    public partial class GeneratorUtils
    {
    }

    /// <summary>
    /// Inspired by https://gamedev.stackexchange.com/questions/140797/check-if-a-game-objects-component-can-be-destroyed
    /// </summary>
    internal static class Extensions
    {
        private static bool Requires(MemberInfo obj, Type requirement)
        {
            return Attribute.IsDefined(obj, typeof(RequireComponent)) &&
                   Attribute.GetCustomAttributes(obj, typeof(RequireComponent)).OfType<RequireComponent>()
                       .Any(requireComponent => requireComponent.m_Type0.IsAssignableFrom(requirement));
        }

        internal static bool CanDestroyAIAction(this GameObject go, Type t)
        {
            return !go.GetComponents<AIAction>().Any(aiAction => Requires(aiAction.GetType(), t));
        }
        
        internal static bool CanDestroyAIDecision(this GameObject go, Type t)
        {
            return !go.GetComponents<AIDecision>().Any(aiDecision => Requires(aiDecision.GetType(), t));
        }
    }

}