using UnityEngine;
using System.Collections;

public class menuNose : MonoBehaviour {
	float speed = 0.07f;
	bool active;
	// Use this for initialization
	void Awake () {
		active = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (active) {
			GetComponent<MenuMotion>().enabled = false;
			Vector2 pos = transform.position;
			transform.position = new Vector2(pos.x + speed,pos.y + 0.04f);
		}
	}
	public void OnTheMove(){
		active = true;
	}
}
