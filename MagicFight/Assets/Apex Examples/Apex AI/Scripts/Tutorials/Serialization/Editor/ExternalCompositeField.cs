/* Copyright © 2014 Apex Software. All rights reserved. */

namespace Apex.Examples.AI.Tutorial
{
    using Apex.AI.Editor;
    using Apex.AI.Editor.Reflection;
    using UnityEditor;
    using UnityEngine;

    [TypesHandled(typeof(ExternalCompositeType))]
    public sealed class ExternalCompositeField : EditorFieldBase<ExternalCompositeType>
    {
        private static readonly GUIContent _text = new GUIContent();

        public ExternalCompositeField(MemberData data, object owner)
            : base(data, owner)
        {
        }

        public sealed override void RenderField(AIInspectorState state)
        {
            if (_curValue == null)
            {
                _text.text = "null";
                EditorGUILayout.LabelField(_label, _text);
                return;
            }

            EditorGUILayout.LabelField(_label);

            EditorGUI.indentLevel += 1;
            _text.text = "Name";
            var name = EditorGUILayout.TextField(_text, _curValue.name);

            _text.text = "Position";
            var pos = EditorGUILayout.Vector3Field(_text, _curValue.position);
            EditorGUI.indentLevel -= 1;

            if (name != _curValue.name || pos != _curValue.position)
            {
                var val = new ExternalCompositeType
                {
                    name = name,
                    position = pos
                };

                UpdateValue(val, state);
            }
        }
    }
}