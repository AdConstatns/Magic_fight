#pragma warning disable 1591

namespace Apex.Examples.AI.Tutorial
{
    using Apex.AI;
    using Apex.Serialization;

    public sealed class SerializationExampleQualifier : QualifierBase
    {
        [ApexSerialization]
        public ICustomType customSettings;

        public override float Score(IAIContext context)
        {
            return 0f;
        }
    }
}