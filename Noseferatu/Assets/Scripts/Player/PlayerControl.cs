using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    private ParticleSystem _sneezeParticleSystem;
    public ParticleSystem sneezeParticleSystem
    {
        get{ return _sneezeParticleSystem ?? (_sneezeParticleSystem = GetComponentInChildren<ParticleSystem>()); }
    }

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
    public float DamageTaken = 0;

    public bool Dead = false;

    private float RegenerateTimer = 0;

    private bool canAttack = true;
    private bool canGetHurt = true;

	// Use this for initialization
	void Start () {
        DamageTaken = 0;
	}
	
	// Update is called once per frame
	void Update () {

        if (DamageTaken > 2) {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            animator.SetTrigger ("dead");
            Dead = true;
            return;
        } else {
            RegenerateTimer += Time.deltaTime;
            if (RegenerateTimer > 20 && DamageTaken > 0) {
                //HOOORAY ! We regenerated !
                DamageTaken -= 1;
                RegenerateTimer = 0;
                //TODO: Some animation
                iTween.PunchScale (headRenderer.gameObject, iTween.Hash (
                    "amount", Vector3.one * 0.05f,
                    "time", 0.3f
                ));
            }
        }

        animator.SetFloat ("Damage", DamageTaken);


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
	}

    void Attack(){
        if (Dead)
            return;
        
        if (canAttack) {
            //extend the nose!!!
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
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

    private void enablerb(){
        rb.isKinematic = false;
    }

    void enableAttack(){
        if (Dead)
            return;
        
        canAttack = true;
        //tween the halo
        iTween.ScaleTo(haloRenderer.gameObject, iTween.Hash(
            "scale", Vector3.one,
            "time", 0.2f,
            "easetype", "spring"
        ));
    }

    void CanBeHurtYes(){
        canGetHurt = true;
    }

    void OnCollisionEnter2D(Collision2D coll){
        if (!canGetHurt || Dead)
            return;
        
        if (coll.gameObject.layer == LayerMask.NameToLayer ("Obstacles")) { //better way to check type?
            animator.SetTrigger("tookDamage");
            DamageTaken += 1;
            RegenerateTimer = 0;
            iTween.PunchScale (headRenderer.gameObject, iTween.Hash (
                "amount", Vector3.one * 0.1f,
                "time", 0.4f
            ));

            iTween.ShakePosition (Camera.main.gameObject, iTween.Hash (
                "amount", Vector3.one * 0.9f,
                "time", 0.6f
            ));

            canGetHurt = false;
            Invoke ("CanBeHurtYes", 2.0f);
        }
    }

    void Sneeze(){
        if (Dead)
            return;
        
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        canAttack = false;
        canGetHurt = false;

        //punch the player sprite scale
        iTween.PunchScale (headRenderer.gameObject, iTween.Hash (
            "amount", Vector3.one * 0.1f,
            "time", 2.4f
        ));

        //this is repeated from the Attack code.. can we encapsulate it?
        iTween.ScaleTo(haloRenderer.gameObject, iTween.Hash(
            "scale", Vector3.zero,
            "time", 0.4f,
            "easetype", "easeincubic"
        ));

        Invoke ("enablerb", 1.3f);
        Invoke ("CanBeHurtYes", 1f);
        Invoke ("enableAttack", 1.3f);

        sneezeParticleSystem.Play ();

        //Make all the obstacles go BOOOOM!
        InvokeRepeating("KillAllObstacles",0,0.2f);
        Invoke ("StopKillingAllObstacles",1.3f);

    }

    void KillAllObstacles(){
        foreach (Obstacle ob in FindObjectsOfType<Obstacle> ()) {
            ob.ExplodeMe ();
        }
    }

    void StopKillingAllObstacles(){
        CancelInvoke ("KillAllObstacles");
    }
}
