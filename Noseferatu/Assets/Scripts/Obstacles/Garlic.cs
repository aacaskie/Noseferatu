using UnityEngine;
using System.Collections;

public class Garlic : Obstacle {

    public GameObject cloudPrefab;

    void Awake(){
        base.Awake ();
        //init pos
        transform.position = new Vector3(
            Camera.main.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect * 1.1f,
            transform.position.y,
            0
        );

    }

	// Use this for initialization
	void Start () {
        rb.AddForce (Vector2.left * 150);
        rb.AddTorque (30);
	}

    void OnHitNose(){
        Instantiate (cloudPrefab, transform.position, Quaternion.identity);
        Destroy (gameObject);
    }

    void OnHitPlayer(){
        Instantiate (cloudPrefab, transform.position, Quaternion.identity);
        Instantiate (EffectManager.Instance.SmallExplosion, transform.position, transform.localRotation);
        Destroy (gameObject);
    }

}
