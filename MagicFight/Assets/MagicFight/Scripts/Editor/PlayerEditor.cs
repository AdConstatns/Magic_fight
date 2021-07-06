namespace AmazingTeam.MagicFight {

	using UnityEngine;
	using System.Collections;
	using UnityEditor;

	[CustomEditor(typeof(Player))]
	public class PlayerEditor : Editor {

		public float viewAngle = 90.0f;
		void OnSceneGUI() {
			Player fow = (Player)target;
			Handles.color = Color.white;
			Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.scanRange);
			Vector3 viewAngleA = fow.DirFromAngle(-viewAngle / 2, false);
			Vector3 viewAngleB = fow.DirFromAngle(viewAngle / 2, false);

			Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * (fow.scanRange));  // View Radius = fow.scanRange/2
			Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * (fow.scanRange));

		}
	}
}
