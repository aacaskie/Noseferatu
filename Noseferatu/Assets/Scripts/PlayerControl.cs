using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    private Rigidbody2D _rb;
    public Rigidbody2D rb {
        get{ return _rb ?? (_rb = GetComponentInChildren<Rigidbody2D> ()); }
    }

    private NoseController _nose;
    public NoseController nose {
        get{ return _nose ?? (_nose = GetComponentInChildren<NoseController> ()); }
    }

    public float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Camera.main.ScreenToWorldPoint (
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)
        ).y > transform.position.y) {
            //move up
            rb.AddForce (Vector2.up * speed);
        } else {
            //move down
            rb.AddForce (Vector2.down * speed);
        }

        if (Input.mousePresent) {
            nose.Rotate (Camera.main.ScreenToWorldPoint (
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)
            ));
        }


        if (Input.GetMouseButtonDown(0)) {
            nose.Extend (Camera.main.ScreenToWorldPoint (
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)
            ));
            rb.isKinematic = true;
        }
	}
}
