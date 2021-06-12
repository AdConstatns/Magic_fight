using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SensorToolkit.Example {

	[CustomEditor(typeof(FieldOfView))]
	public class FieldOfViewEditor : Editor {

		void OnSceneGUI() {
			
			FieldOfView fow = (FieldOfView)target;
			Handles.color = Color.white;
			Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
			Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle / 2, false);
			Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle / 2, false);

			Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
			Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);

			// should not access the destroyed field of view object.
			if(fow.visibleTargets.Count > 0) {
				Handles.color = Color.red;
				//foreach (Transform visibleTarget in fow.visibleTargets) {
				//	Handles.DrawLine(fow.transform.position, visibleTarget.position);
				//}

                for (int i = 0; i < fow.visibleTargets.Count; i++) {
					Handles.DrawLine(fow.transform.position, fow.visibleTargets[i].position);

				}
			}

			
		}

	}

}
