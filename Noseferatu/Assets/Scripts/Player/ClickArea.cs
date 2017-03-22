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
        if (Time.timeScale != 1)
            return;
        
        if (Input.GetMouseButtonDown (0)) {
            SendMessageUpwards ("Attack"); //TRY to attack
        }

        if (Input.GetMouseButtonDown (1)) {
            SendMessageUpwards ("Sneeze");// TRY to sneeze
        }
             

	}
}
