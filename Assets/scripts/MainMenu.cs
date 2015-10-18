using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Camera cam = Camera.main;
		transform.localScale = new Vector3(cam.orthographicSize/2 * (Screen.width/Screen.height),1f, cam.orthographicSize/2);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
