/* Copyright © 2014 Apex Software. All rights reserved. */
#pragma warning disable 1591

namespace Apex.Examples.AI.Tutorial
{
#if UNITY_EDITOR

    using System.Collections.Generic;
    using Apex.AI;
    using Apex.AI.Visualization;
    using UnityEngine;

    public sealed class TargetScoreVisualizerComponent : ActionWithOptionsVisualizerComponent<SetBestAttackTarget, GameObject>
    {
        protected override IList<GameObject> GetOptions(IAIContext context)
        {
            return ((ExampleContext)context).observations;
        }

        protected override void DrawGUI(IList<ScoredOption<GameObject>> scoredOptions)
        {
            if (scoredOptions == null || scoredOptions.Count == 0)
            {
                return;
            }

            var cam = Camera.main;
            if (cam == null)
            {
                return;
            }

            var count = scoredOptions.Count;
            for (int i = 0; i < count; i++)
            {
                var pos = scoredOptions[i].option.transform.position;
                var score = scoredOptions[i].score;

                var p = cam.WorldToScreenPoint(pos);
                p.y = Screen.height - p.y;

                if (score < 0f)
                {
                    GUI.color = Color.red;
                }
                else if (score == 0f)
                {
                    GUI.color = Color.black;
                }
                else
                {
                    GUI.color = Color.green;
                }

                var content = new GUIContent(score.ToString("F0"));
                var size = new GUIStyle(GUI.skin.label).CalcSize(content);
                GUI.Label(new Rect(p.x, p.y, size.x, size.y), content);
            }
        }

        protected override void DrawGizmos(IList<ScoredOption<GameObject>> scoredOptions)
        {            
        }
    }

#endif
}