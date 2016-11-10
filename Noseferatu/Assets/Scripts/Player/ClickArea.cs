using UnityEngine;
using System.Collections;

public class ClickArea : MonoBehaviour {

    private CircleCollider2D _collider;
    public CircleCollider2D collider {
        get{ return _collider ?? (_collider = GetComponentInChildren<CircleCollider2D> ()); }
    }

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown (0)) {
            var col = Physics2D.OverlapPoint (Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (col == collider) {
                //Call the sneeze!
                SendMessageUpwards ("Sneeze");// TRY to sneeze
            } else {
                SendMessageUpwards ("Attack"); //TRY to attack
            }
        }
	}
}
