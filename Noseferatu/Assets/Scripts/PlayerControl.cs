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

    private Animator _animator;
    public Animator animator {
        get{ return _animator ?? (_animator = GetComponentInChildren<Animator> ()); }
    }

    private SpriteRenderer _renderer;

    public SpriteRenderer renderer {
        get{ return _renderer ?? (_renderer = GetComponentInChildren<SpriteRenderer> ()); }
    }

    public float speed;

    public float health;

	// Use this for initialization
	void Start () {
        health = 100;
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

        if (Input.mousePresent && !rb.isKinematic) {
            nose.Rotate (Camera.main.ScreenToWorldPoint (
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)
            ));
        }


        if (Input.GetMouseButtonDown(0)) {

            if (!rb.isKinematic) {
                //extend the nose!!!
                rb.isKinematic = true;

                nose.Extend (
                    Camera.main.ScreenToWorldPoint ((Vector3)Input.mousePosition));

                Invoke ("enablerb", 0.4f); //time needs to be more than the time it takes nose to extend (itween time)
            }

        }


	}

    private void enablerb(){
        rb.isKinematic = false;
    }

    void OnCollisionEnter2D(Collision2D coll){
        if (coll.gameObject.layer == LayerMask.NameToLayer ("Obstacles")) { //better way to check type?
            animator.SetTrigger("tookDamage");
            iTween.PunchScale (renderer.gameObject, iTween.Hash (
                "amount", Vector3.one * 0.1f,
                "time", 0.4f
            ));
        }
    }
}
