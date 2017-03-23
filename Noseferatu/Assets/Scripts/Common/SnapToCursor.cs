using UnityEngine;
using System.Collections;

public class SnapToCursor : MonoBehaviour {

   
	// Use this for initialization
	void Awake () {
        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Camera.main.ScreenToWorldPoint ((Vector3)Input.mousePosition + Vector3.forward * 20);
	}
}
