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

    private SpriteRenderer _spriteRenderer;
    public SpriteRenderer spriteRenderer {
        get{ return _spriteRenderer ?? (_spriteRenderer = GetComponentInChildren<SpriteRenderer> ()); }
    }
        
    public float life;

	// Use this for initialization
	public void Awake () {
        rb.gravityScale = 0f;
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

            //stop the object
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;

            rb.AddForce (Vector2.right * 100 * 3);
            rb.AddForce ( collider.transform.position.y > trigger.transform.position.y ? 
                Vector2.up * 100 : 
                Vector2.down * 100
            );
            rb.AddTorque (-300);
            collider.enabled = false;
        }
    }
}
