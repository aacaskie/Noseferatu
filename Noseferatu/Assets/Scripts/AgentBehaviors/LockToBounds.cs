using UnityEngine;
using System.Collections;
using Common;

[RequireComponent(typeof(Rigidbody2D))]
public class LockToBounds : MonoBehaviour {

    private Rigidbody2D _rb;
    public Rigidbody2D rb {
        get{ return _rb ?? (_rb = GetComponentInChildren<Rigidbody2D> ()); }
    } 

    public float pushForce;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
    void Update () {
        
        Bounds b = Camera.main.OrthographicBounds();
        b.Expand(-Vector3.one * Camera.main.orthographicSize * 0.6f);

        if (b.Contains(transform.position))
        {
            return;
        }
        else
        {
            Vector2 delta = b.ClosestPoint(transform.position) - transform.position;        
            Vector2 force = delta * pushForce;
            rb.AddForce (force);
        }
	}
}
