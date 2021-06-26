/* Copyright � 2014 Apex Software. All rights reserved. */
namespace AmazingTeam.MagicFight {

    using Apex;
    using Apex.Units;
    using Apex.Examples.Extensibility;
    using Apex.Services;
    using Apex.Input;
    using UnityEngine;
    using InControl;

    [AddComponentMenu("MagicFight/Input/Input Receiver Mobile", 1012)]
    [InputReceiver]
    public partial class InputReceiverMobile : MonoBehaviour {

        //IUnitFacade unit;

        public Transform target;
        partial void Steer(Vector3 Direction);

        InputDevice inputDevice;

        bool powerCollected;

        public Vector3 StickDirection { get; set; }

        private bool _IsPressed;
        private bool _IsReleased;

        public bool IsPressed { get { return _IsPressed; } set { _IsPressed = value; } }

        public bool IsReleased { get { return _IsReleased; } set { _IsReleased = value; } }

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

            StickDirection = new Vector3(inputDevice.Direction.X, 0, inputDevice.Direction.Y);

            // Left Stick Pressed
            _IsPressed = inputDevice.LeftStick.IsPressed;
            // Left Stick Released
            _IsReleased = inputDevice.LeftStick.WasReleased;
           

            //Vector3 direction = new Vector3(inputDevice.Direction.X, 0, inputDevice.Direction.Y);
            //Steer(direction);
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