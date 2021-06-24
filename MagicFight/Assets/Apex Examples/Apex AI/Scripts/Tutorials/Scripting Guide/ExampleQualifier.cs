#pragma warning disable 1591

namespace Apex.Examples.AI.Tutorial
{
    using Apex.AI;
    using Apex.Serialization;

    public sealed class ExampleQualifier : QualifierBase
    {
        [ApexSerialization]
        public float score;

        public override float Score(IAIContext context)
        {
            /* Put logic here */

            return this.score;
        }
    }
}