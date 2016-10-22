using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class NoseController : MonoBehaviour {

    public GameObject noseBase;
    public GameObject noseMiddle;
    public GameObject noseTip;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
        noseTip.transform.localPosition = noseMiddle.transform.localPosition + new Vector3(noseMiddle.GetComponentInChildren<BoxCollider2D>().bounds.size.x,0,0);
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
        iTween.Stop(noseMiddle.gameObject, "retract");
        iTween.ScaleTo (noseMiddle.gameObject, iTween.Hash (
            "name", "extend",
            "x", 2f,
            "time", 0.3f,
            "easetype", "spring",
            "oncomplete", "retract",
            "oncompletetarget", this.gameObject
        ));

    }

    private void retract(){
        iTween.ScaleTo (noseMiddle.gameObject, iTween.Hash (
            "name", "retract",
            "x", 0.3f,
            "time", 0.5f,
            "easetype", "easeOutcubic"
        ));
    }


}
