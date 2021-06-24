#pragma warning disable 1591

namespace Apex.Examples.AI.Tutorial
{
    using Apex.AI;
    using UnityEngine;

    public sealed class CustomUtilityAIAction : ActionBase
    {
        public override void Execute(IAIContext context)
        {
            // there is no setup to support actual doing something meaningful on a non-main thread in Unity
            // thus, this action does nothing, since it is executed on a dedicated thread - it is just to exemplify an action
            var calc = 2 + 2;
            if (calc == 4)
            {
                calc++;
            }

            Debug.Log(calc);
        }
    }
}