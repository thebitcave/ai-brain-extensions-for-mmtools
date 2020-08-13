using UnityEngine;

namespace TheBitCave.MMToolsExtensions
{
    /// <summary>
    /// This ScriptableObject is used as a channel Id to filter State change commands from a Slave AIBrain.
    /// It doesn't bring data as it is used just the instance as an identifier.
    /// </summary>
    [CreateAssetMenu(menuName = "The Bit Cave/MasterSlave/State Command Channel", fileName = "New State Command Channel")]
    public class StateCommandChannel : ScriptableObject
    {
    }
}