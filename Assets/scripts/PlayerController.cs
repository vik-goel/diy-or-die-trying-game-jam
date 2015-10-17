using UnityEngine;
using System.Collections;

using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

	public float speed = 10;
	float rotx = 0, roty = 0;

	Rigidbody myRigidBody;
	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody> ();
	}

	public static float ClampAngle (float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp (angle, min, max);
	}

	// Update is called once per frame
	void FixedUpdate () {
		rotx += CrossPlatformInputManager.GetAxis ("Horizontal") * 3;
		roty += CrossPlatformInputManager.GetAxis ("Vertical") * 3;

		//if (roty > 40)
			//roty = 40;

		roty = ClampAngle (roty, -60, 60);

		Quaternion qx = Quaternion.AngleAxis (rotx, Vector3.up);
		Quaternion qy = Quaternion.AngleAxis (roty, Vector3.left);

		transform.localRotation = qx * qy;

		/*
		//transform.LookAt (lookVec);

		Vector3 rot1 = new Vector3 (0f, CrossPlatformInputManager.GetAxis ("Vertical"), 0f);
		Quaternion q1 = Quaternion.LookRotation (rot1);
		Vector3 rot2 = new Vector3 (CrossPlatformInputManager.GetAxis ("Horizontal"), 0f, 0f);
		Quaternion q2 = Quaternion.LookRotation (rot2);
		Debug.Log (q2);
		//transform.rotation = Quaternion.Slerp(transform.rotation, q1, 5 * Time.deltaTime);
		transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation * q2 * q1, Time.deltaTime);

		//Matrix4x4 result = Matrix4x4.TRS(new Vector3(0, 0, 0), q1, new Vector3(1, 1, 1))*Matrix4x4.TRS(new Vector3(0, 0, 0), q2, new Vector3(1, 1, 1));
		//Quaternion.

		//transform.rotation = Quaternion.LookRotation(new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, 0f));
		//transform.rotation = new Quaternion (transform.rotation.x, transform.rotation.y, 0, transform.rotation.w);
		transform.rotation = Quaternion.LookRotation (transform.forward);*/


	/*	Vector3 lookVec = new Vector3 (
		                               CrossPlatformInputManager.GetAxis("Vertical")* 5f, 0f,
		                               0f);
		Vector3 turnVec = new Vector3 (0f, 
		                               CrossPlatformInputManager.GetAxis("Horizontal") * 0.6f, 
		                               0f);
		transform.Rotate (turnVec);
		Debug.Log (transform.rotation.x);
		if (transform.eulerAngles.x < 50) {
			transform.Rotate(lookVec);
		}
*/
		//float theta = Mathf.Rad2Deg*Mathf.Atan2 (CrossPlatformInputManager.GetAxis ("Vertical"), CrossPlatformInputManager.GetAxis ("Horizontal"));

		//transform.rotation = Quaternion.AngleAxis (theta, transform.forward);

		 /*var joystickHorizontal = UnityStandardAssets.CrossPlatformInput.CrossPlatformInputManager.GetAxis("Horizontal"); 
		var joystickVertical = UnityStandardAssets.CrossPlatformInput.CrossPlatformInputManager.GetAxis("Vertical"); 

		if (joystickHorizontal != 0 && joystickVertical != 0) {
			var angle = Mathf.Atan2(joystickVertical , joystickHorizontal ) * Mathf.Rad2Deg; 
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); 
		}*/
		//Quaternion.FromToRotation (transform.forward, new Vector3 (CrossPlatformInputManager.GetAxis ("Vertical"), CrossPlatformInputManager.GetAxis ("Horizontal"), 0));
	}
}
