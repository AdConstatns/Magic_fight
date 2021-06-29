/* Copyright © 2021 AmazingTeam Software. All rights reserved. */
namespace AmazingTeam.MagicFight {

    using Apex.Input;
    using UnityEngine;
    using InControl;

    [AddComponentMenu("MagicFight/Input/Input Receiver Mobile", 0)]
    [InputReceiver]
    public partial class InputReceiverMobile : MonoBehaviour {

        //IUnitFacade unit;

        public Transform target;
        partial void Steer(Vector3 Direction);

        InputDevice inputDevice;

        bool powerCollected;

        public Vector3 LeftStickDirection { get; set; }

        private bool _IsPressed;
        private bool _WasReleased;

        public bool IsPressed { get { return _IsPressed; } private set { _IsPressed = value; } }

        public bool WasReleased { get { return _WasReleased; } private set { _WasReleased = value; } }

        private void Awake() {

            // Disable and hide touch controls if we use a controller.
            // If "Enable Controls On Touch" is ticked in Touch Manager inspector,
            // controls will be enabled and shown again when the screen is touched.
            if (inputDevice != InputDevice.Null && inputDevice != TouchManager.Device) {
                TouchManager.ControlsEnabled = false;
            }

            //unit = this.GetUnitFacade();
        }

        private void Update() {

            // Use last device which provided input.
            inputDevice = InputManager.ActiveDevice;

            if (inputDevice is null)
                return;         

            LeftStickDirection = new Vector3(inputDevice.Direction.X, 0, inputDevice.Direction.Y);

            // Left Stick Pressed
            _IsPressed = inputDevice.LeftStick.IsPressed;
            // Left Stick Released
            _WasReleased = inputDevice.LeftStick.WasReleased;      
        }     

        partial void Steer(Vector3 direction) {            

            //if (inputDevice.LeftStick.IsPressed) {
            //    // LeftStick is pressed
            //    unit.MoveTo(this.transform.position + (direction), false);
            //}  else if (inputDevice.LeftStick.WasReleased) { 
            //    // If LeftStick was Released
            //}

            //// If Joystick was released and power is collected. Then attack.
            //if (inputDevice.LeftStick.WasReleased && powerCollected) {
            //    // If LeftStick was Released after collecting the power.
            //}
        }      
    }
}
