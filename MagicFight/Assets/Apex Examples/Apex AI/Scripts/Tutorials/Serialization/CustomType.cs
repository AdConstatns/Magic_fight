/* Copyright © 2014 Apex Software. All rights reserved. */
#pragma warning disable 1591

namespace Apex.Examples.AI.Tutorial
{
    using Apex.AI;
    using Apex.Serialization;
    using UnityEngine;

    public class CustomType : ICustomType
    {
        [ApexSerialization(defaultValue = true)]
        protected bool _showDetails = true;

        [ApexSerialization, FriendlyName("Name")]
        public string nameField;

        [ApexSerialization(defaultValue = 5), MemberCategory("Details"), MemberDependency("_showDetails", true)]
        public int number = 5;

        [ApexSerialization, MemberCategory("Details"), MemberDependency("_showDetails", true)]
        public Vector3 position
        {
            get;
            set;
        }
    }
}