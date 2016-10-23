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

    public SpriteRenderer headRenderer;
    public SpriteRenderer haloRenderer;

    public float Speed;
    public float Health;

    private bool canAttack = true;

	// Use this for initialization
	void Start () {
        Health = 100;
	}
	
	// Update is called once per frame
	void Update () {

        if (Health <= 0) {
            rb.isKinematic = true;
            animator.SetTrigger ("dead");
            return;
        }


        if (Camera.main.ScreenToWorldPoint (
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)
        ).y > transform.position.y) {
            //move up
            rb.AddForce (Vector2.up * Speed);
        } else {
            //move down
            rb.AddForce (Vector2.down * Speed);
        }

        if (Input.mousePresent && canAttack) {
            nose.Rotate (Camera.main.ScreenToWorldPoint (
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)
            ));
        }


        if (Input.GetMouseButtonDown(0)) {

            if (canAttack) {
                //extend the nose!!!
                rb.isKinematic = true;
                canAttack = false;

                iTween.ScaleTo(haloRenderer.gameObject, iTween.Hash(
                    "scale", Vector3.zero,
                    "time", 0.4f,
                    "easetype", "easeincubic"
                ));

                nose.Extend (
                    Camera.main.ScreenToWorldPoint ((Vector3)Input.mousePosition));

                Invoke ("enablerb", 0.4f); //time needs to be more than the time it takes nose to extend (itween time)
                Invoke ("enableAttack", 1.0f);
            }

        }


	}

    private void enablerb(){
        rb.isKinematic = false;
    }

    void enableAttack(){
        canAttack = true;
        //tween the halo
        iTween.ScaleTo(haloRenderer.gameObject, iTween.Hash(
            "scale", Vector3.one,
            "time", 0.2f,
            "easetype", "spring"
        ));
    }

    void OnCollisionEnter2D(Collision2D coll){
        if (coll.gameObject.layer == LayerMask.NameToLayer ("Obstacles")) { //better way to check type?
            animator.SetTrigger("tookDamage");
            Health -= 10;
            iTween.PunchScale (headRenderer.gameObject, iTween.Hash (
                "amount", Vector3.one * 0.1f,
                "time", 0.4f
            ));
        }
    }
}
