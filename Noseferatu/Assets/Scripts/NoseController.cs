using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class NoseController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Rotate(Vector2 target){
//        Vector3 delta = ((Vector3)target - transform.position).normalized;
//        float angle = Vector3.Angle (delta, Vector3.right);
//        Debug.Log ("angle:" + angle.ToString ());
        transform.right = ((Vector3)target - transform.position);
    }

    public void Extend(Vector2 target){
        Rotate (target);
        //itween shit
        iTween.Stop(gameObject, "retract");
        iTween.ScaleTo (gameObject, iTween.Hash (
            "name", "extend",
            "x", 2f,
            "time", 0.3f,
            "easetype", "spring",
            "oncomplete", "retract"
        ));

    }

    private void retract(){
        iTween.ScaleTo (gameObject, iTween.Hash (
            "name", "retract",
            "x", 0.3f,
            "time", 0.5f,
            "easetype", "easeOutcubic"
        ));
    }
}
