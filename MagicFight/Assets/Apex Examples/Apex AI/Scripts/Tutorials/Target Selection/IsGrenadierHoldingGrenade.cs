#pragma warning disable 1591

namespace Apex.Examples.AI.Tutorial
{
    using Apex.AI;
    using Apex.Serialization;
    using UnityEngine;

    public sealed class IsGrenadierHoldingGrenade : OptionScorerBase<GameObject>
    {
        [ApexSerialization]
        public float score = 50f;

        public override float Score(IAIContext context, GameObject option)
        {
            // components should normally be cached and not 'get' on every tick, simplified here for tutorial purposes
            var weapon = option.GetComponent<WeaponComponent>();
            if (weapon.IsHoldingGrenade())
            {
                return this.score;
            }

            return 0f;
        }
    }
}