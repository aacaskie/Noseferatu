using UnityEngine;
using System.Collections;

public class Knife : Obstacle {

    void Awake(){
        base.Awake ();
        //init pos
        transform.position = new Vector3(
            Camera.main.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect * 1.1f,
            transform.position.y,
            0
        );

    }

    // Use this for initialization
    void Start () {
        rb.AddForce (Vector2.right * 140);
        rb.AddTorque (100);
        Invoke ("push", 2.0f);
        spriteRenderer.color = Color.black;
        transform.localScale = transform.localScale * 0.5f;
        collider.enabled = false;

        spriteRenderer.sortingOrder = -1;

        iTween.ScaleTo (gameObject, iTween.Hash(
            "scale", transform.localScale * 2,
            "time", 1f,
            "delay", 1.6f
        ));

        iTween.ValueTo (gameObject, iTween.Hash (
            "from", Color.black,
            "to", Color.white,
            "time", 0.2f,
            "delay", 1.5f,
            "onUpdate", "updateColor"
        ));

    }

    // Update is called once per frame
    void FixedUpdate () {
        rb.AddForce (Vector2.left * 1.1f);
    }

    void OnHitNose(){
        base.PushAway ();
    }

    void OnHitPlayer(){
        Instantiate (EffectManager.Instance.SmallExplosion, transform.position, transform.localRotation);
        collider.enabled = false;
        rb.AddTorque (100);
        rb.AddForce (Vector2.left * 100);
    }

    IEnumerator ClearMe(){
        yield return new WaitForSeconds (0.05f);
        Destroy (gameObject);
    }

    void push(){
        spriteRenderer.sortingOrder = 1;

        rb.AddTorque (200);
        rb.AddForce (Vector2.left * 200);
        collider.enabled = true;
    }

    void updateColor(Color col){
        spriteRenderer.color = col;
    }

}
