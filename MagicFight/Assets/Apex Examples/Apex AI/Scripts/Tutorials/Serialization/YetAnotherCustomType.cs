/* Copyright © 2014 Apex Software. All rights reserved. */
#pragma warning disable 1591
#pragma warning disable 169

namespace Apex.Examples.AI.Tutorial
{
    using Apex.Serialization;

    public class YetAnotherCustomType : ICustomType
    {
        [ApexSerialization]
        private ExternalPrimitiveType _primitive;

        public YetAnotherCustomType()
        {
            this.namedPosition = new ExternalCompositeType();
        }

        [ApexSerialization]
        public ExternalCompositeType namedPosition
        {
            get;
            set;
        }
    }
}