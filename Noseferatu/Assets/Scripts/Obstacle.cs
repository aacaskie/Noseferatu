using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

    private Collider2D _collider;
    public Collider2D collider {
        get{ return _collider ?? (_collider = GetComponentInChildren<Collider2D> ()); }
    }

    private Rigidbody2D _rb;
    public Rigidbody2D rb {
        get{ return _rb ?? (_rb = GetComponentInChildren<Rigidbody2D> ()); }
    }

    public float speed;
    public float life;

	// Use this for initialization
	void Awake () {
        rb.gravityScale = 0f;
        rb.AddForce (Vector2.left * speed);
	}
	
	// Update is called once per frame
	void Update () {

        //countdown and destroy
        life -= Time.deltaTime;
        if (life < 0) {
            Destroy (this.gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D trigger){
        if (trigger.CompareTag ("nose")) {
            rb.AddForce (Vector3.right * speed * 2);
            rb.AddTorque (-100);
        }

    }
}
