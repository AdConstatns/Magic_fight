namespace AmazingTeam.MagicFight {

	using UnityEngine;
	using System.Collections;
	using UnityEditor;

	[CustomEditor(typeof(Player))]
	public class PlayerEditor : Editor {

		public float viewAngle = 90.0f;
		void OnSceneGUI() {
			Player player = (Player)target;
			Handles.color = Color.white;
			Handles.DrawWireArc(player.transform.position, Vector3.up, Vector3.forward, 360, player.scanRange);
			Vector3 viewAngleA = player.DirFromAngle(-viewAngle / 2, false);
			Vector3 viewAngleB = player.DirFromAngle(viewAngle / 2, false);

			Handles.DrawLine(player.transform.position, player.transform.position + viewAngleA * (player.scanRange));  // View Radius = fow.scanRange/2
			Handles.DrawLine(player.transform.position, player.transform.position + viewAngleB * (player.scanRange));

		}
	}
}
