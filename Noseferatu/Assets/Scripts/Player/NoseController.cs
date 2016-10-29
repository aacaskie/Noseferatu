using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class NoseController : MonoBehaviour {

    public GameObject NoseTipSprite;

    private Collider2D _collider;
    public Collider2D collider {
        get{ return _collider ?? (_collider = GetComponentInChildren<Collider2D> ()); }
    }

	// Use this for initialization
	void Awake () {
        collider.enabled = false;
	}
	
	// Update is called once per frame

    public void Rotate(Vector2 target){
//        Vector3 delta = ((Vector3)target - transform.position).normalized;
//        float angle = Vector3.Angle (delta, Vector3.right);
//        Debug.Log ("angle:" + angle.ToString ());
        transform.right = ((Vector3)target - transform.position);
    }

    public void Extend(Vector2 target){
        collider.enabled = true;
        Rotate (target);
        //itween shit
        iTween.Stop(NoseTipSprite.gameObject, "retract");
        iTween.ScaleTo (NoseTipSprite.gameObject, iTween.Hash (
            "name", "extend",
            "x", 1f,
            "time", 0.3f,
            "easetype", "spring",
            "oncomplete", "retract",
            "oncompletetarget", this.gameObject
        ));

    }

    private void retract(){
        iTween.ScaleTo (NoseTipSprite.gameObject, iTween.Hash (
            "name", "retract",
            "x", 0.1f,
            "time", 0.5f,
            "easetype", "easeOutcubic",
            "oncomplete", "disableCollider",
            "oncompleteTarget", gameObject
        ));
    }

    private void disableCollider(){
        collider.enabled = false;
    }

}
