/* Copyright © 2014 Apex Software. All rights reserved. */

namespace Apex.Examples.AI.Tutorial
{
    using Apex.AI.Editor;
    using Apex.AI.Editor.Reflection;
    using UnityEditor;

    [TypesHandled(typeof(ExternalPrimitiveType))]
    public sealed class ExternalPrimitiveField : EditorFieldBase<ExternalPrimitiveType>
    {
        public ExternalPrimitiveField(MemberData data, object owner)
            : base(data, owner)
        {
        }

        public sealed override void RenderField(AIInspectorState state)
        {
            var val = EditorGUILayout.FloatField(_label, _curValue.value, EditorStyles.numberField);
            if (val != _curValue.value)
            {
                UpdateValue(new ExternalPrimitiveType(val), state);
            }
        }
    }
}