using UnityEngine;
using System.Collections;
using InControl;
using SensorToolkit.Example;

namespace SensorToolkit.Example {
    [RequireComponent(typeof(PlayerCharacterControls))]
    public class PlayerInput : MonoBehaviour {
        public Sensor InteractionRange;
        PlayerCharacterControls cc;
        InputDevice inputDevice;

        PlayerHolder _playerHolder;
        Animator _animator;

        FieldOfView fieldOfView;

        GunWithClip gun;

        private void Awake() {
            cc = GetComponent<PlayerCharacterControls>();
            _playerHolder = GetComponentInChildren<PlayerHolder>();
            _animator = GetComponentInChildren<Animator>();
            fieldOfView = GetComponentInChildren<FieldOfView>();
            gun = GetComponent<GunWithClip>();

        }

        void Start() {

            // Disable and hide touch controls if we use a controller.
            // If "Enable Controls On Touch" is ticked in Touch Manager inspector,
            // controls will be enabled and shown again when the screen is touched.
            if (inputDevice != InputDevice.Null && inputDevice != TouchManager.Device) {
                TouchManager.ControlsEnabled = false;
            }

            // Setting the Collectable's IsHeld property to zero.
            _playerHolder.IsHeld = false;
        }

        void Update() {
            //var horiz = Input.GetAxis("Horizontal");
            //var vert = Input.GetAxis("Vertical");

            // Use last device which provided input.
            inputDevice = InputManager.ActiveDevice;

            var horiz = inputDevice.Direction.X;
            var vert = inputDevice.Direction.Y;

            cc.Move = new Vector3(horiz, 0f, vert);

            var playerDir = new Vector3(inputDevice.Direction.X, 0, inputDevice.Direction.Y);


            if (inputDevice.LeftStick.IsPressed) {
                Debug.Log("PlayerInput: " + playerDir);
                cc.isLeftStickPressed = true;
                cc.Move = playerDir;
                cc.Face = playerDir;
                _animator.SetBool("Walk", true);
            } else if (inputDevice.LeftStick.WasReleased) {
                cc.isLeftStickPressed = false;
                _animator.SetBool("Walk", false);
            }

            // If Joystick was released and power is collected. Then attack.
            if (inputDevice.LeftStick.WasReleased && _playerHolder.IsHeld) {              
                _animator.SetBool("IsPower", true);
                gun.Fire();
            } else {
                _animator.SetBool("IsPower", false);
            }

            // Enabling and Disabling the Field of View Script
            if (_playerHolder.IsHeld) {
                fieldOfView.enabled = true;
            }               
            else {
                fieldOfView.enabled = false;
            }

            // Pickup the pickup if its in range
            var pickup = InteractionRange.GetNearestByComponent<Holdable>();
            if (pickup != null && !pickup.IsHeld) {
                cc.PickUp(pickup);
            }
        }
    }
}