#pragma warning disable 1591

namespace Apex.Examples.AI.Tutorial
{
    using System.Collections.Generic;
    using Apex.AI;

    public sealed class ExampleSelector : Selector
    {
        public ExampleSelector()
            : base()
        {
        }

        public override IQualifier Select(IAIContext context, IList<IQualifier> qualifiers, IDefaultQualifier defaultQualifier)
        {
            /* Put logic here */

            return defaultQualifier;
        }
    }
}