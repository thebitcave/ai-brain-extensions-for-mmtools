﻿using MoreMountains.Feedbacks;
using UnityEngine;

namespace TheBitCave.MMToolsExtensions
{
    /// <summary>
    /// An engine agnostic, scaled-down ability component. Lets you work with feedbacks. 
    /// </summary>
    public class SimpleAbility : MonoBehaviour
    {
        /// This method is only used to display a helpbox text at the beginning of the ability's inspector
        public virtual string HelpBoxText() { return ""; }

        public MMFeedbacks AbilityFeedbacks;

        /// <summary>
        /// On Start(), we call the ability's intialization
        /// </summary>
        protected virtual void Start () 
        {
            Initialization();
        }

        /// <summary>
        /// Gets and stores components for further use
        /// </summary>
        protected virtual void Initialization()
        {
        }
        
        /// <summary>
        /// Plays the ability start sound effect
        /// </summary>
        protected virtual void PlayAbilityFeedbacks()
        {
            if (AbilityFeedbacks == null) return;
            AbilityFeedbacks.PlayFeedbacks(this.transform.position);
        }	

        protected virtual void OnEnable()
        {
        }
        
        protected virtual void OnDisable()
        {
        }

    }
}