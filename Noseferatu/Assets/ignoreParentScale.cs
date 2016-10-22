using UnityEngine;
using System.Collections;

public class ignoreParentScale : MonoBehaviour {

    private Vector3 initScale;
	// Use this for initialization
	void Awake () {
	    //grab initial local scale
        initScale = transform.localScale;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (transform.parent) {
            transform.localScale = new Vector3 (
                initScale.x / transform.parent.localScale.x,
                initScale.y / transform.parent.localScale.y,
                1);
        }
	}
}
