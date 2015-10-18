using UnityEngine;
using System.Collections;

using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float rotateXSensitivity;
	public float rotateYSensitivity;
	public float noMovementBuffer;

	public float minYAngle;
	public float maxYAngle;

	float rotX = 0, rotY = 0;
	bool buttonDown;

	Rigidbody myRigidBody;
	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody> ();
		buttonDown = false;
	}

	float ClampAngle (float angle, float min, float max) {
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp (angle, min, max);
	}

	float clampRotationAxis(float axis) {
		if (Mathf.Abs (axis) > noMovementBuffer) {
			if (axis > 0) {
				return axis - noMovementBuffer;
			} else {
				return axis + noMovementBuffer;
			}
		}

		return 0;
	}

	// Update is called once per frame
	void FixedUpdate () {
		float axisX = clampRotationAxis(CrossPlatformInputManager.GetAxis ("Horizontal"));
		float axisY = clampRotationAxis(CrossPlatformInputManager.GetAxis ("Vertical"));

		rotX += axisX * rotateXSensitivity * Time.fixedDeltaTime;
		rotY +=  axisY * rotateYSensitivity * Time.fixedDeltaTime;

		rotY = ClampAngle (rotY, minYAngle, maxYAngle);

		Quaternion qx = Quaternion.AngleAxis (rotX, Vector3.up);
		Quaternion qy = Quaternion.AngleAxis (rotY, Vector3.left);

		transform.localRotation = qx * qy;

		if (buttonDown) {
			transform.position += new Vector3(transform.forward.x, 0,transform.forward.z) * Time.fixedDeltaTime * speed;
		} else {
			myRigidBody.velocity = new Vector3(0, myRigidBody.velocity.y, 0);
		}
	}

	public void walk (){
		buttonDown = true;
	}
	public void dontWalk(){
		buttonDown = false;
	}
}
