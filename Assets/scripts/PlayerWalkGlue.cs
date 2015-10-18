using UnityEngine;
using System.Collections;

public class PlayerWalkGlue : MonoBehaviour {

	PlayerController player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	bool getPlayer() {
		if (player == null) {
			GameObject obj = GameObject.FindWithTag("LocalPlayer");
			player = obj.GetComponent<PlayerController>();
		}

		return player != null;
	}

	public void walk() {
		if (getPlayer ()) {
			player.walk ();
		}
	}

	public void dontWalk() {
		if (getPlayer ()) {
			player.dontWalk();
		}
	}
}
