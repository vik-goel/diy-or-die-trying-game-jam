using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

	GameObject player;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LateUpdate() {
		if (player == null) {
			player = GameObject.FindWithTag ("LocalPlayer");
		}

		if (player != null) {
			transform.rotation = player.transform.rotation;
			transform.position = player.transform.position;
		}
	}
}
