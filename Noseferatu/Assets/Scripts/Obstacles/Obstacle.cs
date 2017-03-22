using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

    private ParticleSystem _ExplodeParticles;
    public ParticleSystem ExplodeParticles {
        get{ return _ExplodeParticles ?? (_ExplodeParticles = GetComponentInChildren<ParticleSystem> ()); }
    }

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
	public void Update () {

        //countdown and destroy
        life -= Time.deltaTime;
        if (life < 0) {
            Destroy (this.gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D trigger){
        if (trigger.CompareTag ("nose")) {
            SendMessage ("OnHitNose", trigger);
        }

    }

    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag ("Player")) {
            SendMessage ("OnHitPlayer", collider);
        }
    }

    public void PushAway(Collider2D trigger = null){
        //stop the object
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;

        rb.AddForce (Vector2.right * 100 * 3);

        if (trigger) {
            rb.AddForce (collider.transform.position.y > trigger.transform.position.y ? 
            Vector2.up * 100 : 
            Vector2.down * 100
            );
        }

        rb.AddTorque (-300);
        collider.enabled = false;

        life = 2;
    }

    public virtual void ExplodeMe(){
        DestroyMe ();
    }

    public void DestroyMe(){
        Destroy (this.gameObject);
    }
}
