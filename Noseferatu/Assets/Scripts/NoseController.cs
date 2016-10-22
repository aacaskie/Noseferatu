using UnityEngine;
using System.Collections;

public class NoseController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Rotate(Vector2 target){
        transform.right = (Vector3)target - transform.position;
    }

    public void Extend(Vector2 target){
        Rotate (target);
        //itween shit
    }
}
