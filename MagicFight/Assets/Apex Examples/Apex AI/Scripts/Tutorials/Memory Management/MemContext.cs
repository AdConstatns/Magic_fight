#pragma warning disable 1591

namespace Apex.Examples.AI.Tutorial
{
    using Apex.AI;

    public class MemContext : IAIContext
    {
        public MemContext(IMemEntity owner)
        {
            this.entity = owner;
        }

        public IMemEntity entity
        {
            get;
            private set;
        }
    }
}
