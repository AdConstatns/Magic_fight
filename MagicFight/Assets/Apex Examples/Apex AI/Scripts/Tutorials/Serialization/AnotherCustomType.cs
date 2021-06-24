/* Copyright © 2014 Apex Software. All rights reserved. */
#pragma warning disable 1591

namespace Apex.Examples.AI.Tutorial
{
    using Apex.Serialization;

    [ApexSerializedType]
    public class AnotherCustomType : ICustomType
    {
        /* Since this type has no state to serialize, it has been decorated by the ApexSerializedTypeAttribute
           to indicate that it is a serialized type. This mainly serves the purpose of allowing certain tools to find it */
    }
}