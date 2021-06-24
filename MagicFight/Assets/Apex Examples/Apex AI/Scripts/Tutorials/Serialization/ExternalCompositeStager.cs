/* Copyright © 2014 Apex Software. All rights reserved. */
#pragma warning disable 1591

namespace Apex.Examples.AI.Tutorial
{
    using System;
    using Apex.Serialization;
    using UnityEngine;

    public class ExternalCompositeStager : IStager
    {
        public Type[] handledTypes
        {
            get
            {
                return new[] { typeof(ExternalCompositeType) };
            }
        }

        public StageItem StageValue(string name, object value)
        {
            var val = (ExternalCompositeType)value;

            return new StageElement(
                name,
                SerializationMaster.ToStageAttribute("name", val.name),
                SerializationMaster.Stage("position", val.position));
        }

        public object UnstageValue(StageItem item, Type targetType)
        {
            var element = (StageElement)item;
            return new ExternalCompositeType
            {
                name = element.AttributeValueOrDefault<string>("name"),
                position = SerializationMaster.Unstage<Vector3>(element.Item("position"))
            };
        }
    }
}