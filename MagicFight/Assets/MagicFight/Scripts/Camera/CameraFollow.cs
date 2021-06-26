namespace AmazingTeam.MagicFight
{
    using UnityEngine;

    public class CameraFollow : MonoBehaviour
    {
        public Transform target;            // The position that that camera will be following.
        public float smoothing = 5f;        // The speed with which the camera will be following.

        private Vector3 _offset = new Vector3(-0.2f, 15f, -26.7f);           // The initial offset from the target.
        //private Vector3 _offset = new Vector3(0.0f, 0.0f, 0.0f);

        private void Start() {
#if UNITY_EDITOR
            Debug.Log("<color=yellow><b>Initializing: Camera </b></color>");
#endif
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }

        private void FixedUpdate()
        {
            if (target == null)
            {
                return;
            }

            // Create a position the camera is aiming for based on the offset from the target.
            Vector3 targetCamPos = target.position + _offset;

            // Smoothly interpolate between the camera's current position and it's target position.
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        }
    }
}