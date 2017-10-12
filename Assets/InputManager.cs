using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class InputManager {
	public static float getAxis(XboxAxis axis, XboxController controller) {
		float keyboardAxis = 0;
		switch(controller) {
			case XboxController.First:
				switch(axis) {
					case XboxAxis.LeftStickX:
						keyboardAxis = Input.GetAxis("arrowsX");
						break;
					case XboxAxis.LeftStickY:
						keyboardAxis = Input.GetAxis("arrowsY");
						break;

					case XboxAxis.RightStickX:
						keyboardAxis = Input.GetAxis("numpadX");
						break;
					case XboxAxis.RightStickY:
						keyboardAxis = Input.GetAxis("numpadY");
						break;
				}
				break;
			case XboxController.Second:
				switch (axis) {
					case XboxAxis.LeftStickX:
						keyboardAxis = Input.GetAxis("wasdX");
						break;
					case XboxAxis.LeftStickY:
						keyboardAxis = Input.GetAxis("wasdY");
						break;

					case XboxAxis.RightStickX:
						keyboardAxis = Input.GetAxis("ijklX");
						break;
					case XboxAxis.RightStickY:
						keyboardAxis = Input.GetAxis("ijklY");
						break;
				}
				break;
		}
		
		float controllerAxis = XCI.GetAxis(axis, controller);

		if (Mathf.Abs(keyboardAxis) > Mathf.Abs(controllerAxis)) {
			return keyboardAxis;
		} else {
			return controllerAxis;
		}
	}
}
