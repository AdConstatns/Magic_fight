#pragma warning disable 1591

namespace Apex.Examples.AI.Tutorial
{
    using System;
    using Apex.AI.Components;

    /// <summary>
    /// A specialized UtilityAIClient which registers itself with the <see cref="UtilityAIThreadManager"/>.
    /// </summary>
    public class ThreadedUtilityAIClient : UtilityAIClient
    {
        public bool isPaused;

        public ThreadedUtilityAIClient(Guid aiId, IContextProvider contextProvider)
            : base(aiId, contextProvider)
        {
        }

        protected override void OnStart()
        {
            UtilityAIThreadManager.RegisterClient(this);
        }

        protected override void OnStop()
        {
            UtilityAIThreadManager.UnregisterClient(this);
        }

        protected override void OnPause()
        {
            this.isPaused = true;
        }

        protected override void OnResume()
        {
            this.isPaused = false;
        }
    }
}