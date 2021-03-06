/* Copyright © 2014 Apex Software. All rights reserved. */

namespace Apex.Editor
{
    using System.Collections.Generic;
    using System.Reflection;
    using Apex.Steering;
    using Apex.Steering.Components;
    using UnityEditor;

    [CustomEditor(typeof(OrientationComponent), true), CanEditMultipleObjects]
    public class DefaultOrientationComponentEditor : OrientationComponentEditor
    {
        private List<SerializedProperty> _props;

        protected override void CreateUI()
        {
            base.CreateUI();

            EditorGUILayout.Separator();
            foreach (var p in _props)
            {
                EditorGUILayout.PropertyField(p);
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            _props = this.GetProperties();
        }
    }
}
