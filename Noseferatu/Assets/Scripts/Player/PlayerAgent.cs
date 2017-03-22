using UnityEngine;
using System.Collections;

public class PlayerAgent : MonoBehaviour {

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

    private float RegenerateTimer = 0;

    private bool canAttack = true;
    private bool canGetHurt = true;

	// Use this for initialization
	void Start () {
        DialogManager.Instance.StartDialog (new string[]{
            "Hello!, this is going to be a longer message. Does it towkr?",
            "Another fucking line of text, and we wonder where we went wrong? It's so obvious. We need to do battle",
            "I can't believe you would do this to me!!!!"
        });
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        //MOVEMENT
        if (Camera.main.ScreenToWorldPoint (
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)
        ).y > transform.position.y) {
            //move up
            rb.AddForce (Vector2.up * Speed);
        } else {
            //move down
            rb.AddForce (Vector2.down * Speed);
        }

        //Regenerate loop
        RegenerateTimer += Time.deltaTime;
        if (RegenerateTimer > 20 && Game.Instance.PlayerInfo.Health < Game.Instance.PlayerInfo.MaxHealth) {
            //HOOORAY ! We regenerated !
            Game.Instance.PlayerInfo.Health += 1;
            RegenerateTimer = 0;
            //TODO: Some animation
            iTween.PunchScale (headRenderer.gameObject, iTween.Hash (
                "amount", Vector3.one * 0.05f,
                "time", 0.3f
            ));
        }
            
        //NOSE DIRECTION
        if (Input.mousePresent && canAttack) {
            nose.Rotate (Camera.main.ScreenToWorldPoint (
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)
            ));
        }

        //ANIMATOR
        animator.SetFloat ("Health", Game.Instance.PlayerInfo.Health);
	}

    void Attack(){
        if (!canAttack) return;

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

    void CanBeHurtYes(){
        canGetHurt = true;
    }

    void OnCollisionEnter2D(Collision2D coll){
        if (!canGetHurt)
            return;
        
        if (coll.gameObject.layer == LayerMask.NameToLayer ("Obstacles")) { //better way to check type?
            animator.SetTrigger("tookDamage");

            Game.Instance.PlayerInfo.TakeDamage(1);

            SoundManager.Instance.PlaySound ("PlayerHit");

            Game.Instance.SlowTime (0.6f);

            RegenerateTimer = 0;

            iTween.PunchScale (headRenderer.gameObject, iTween.Hash (
                "amount", Vector3.one * 0.1f,
                "time", 0.4f
            ));

            canGetHurt = false;
            Invoke ("CanBeHurtYes", 1.0f);
        }
    }

    void Sneeze(){
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
            print (ob.name);
            ob.ExplodeMe ();
        }
    }

    void StopKillingAllObstacles(){
        CancelInvoke ("KillAllObstacles");
    }
}
