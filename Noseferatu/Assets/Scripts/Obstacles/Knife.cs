using UnityEngine;
using System.Collections;

public class Knife : Obstacle {

    private Animator _animator;

    public Animator animator {
        get{ return _animator ?? (_animator = GetComponentInChildren<Animator> ()); }
    }


	// Use this for initialization
	void Start () {
        rb.AddTorque (100);
	}
	
	// Update is called once per frame
	void Update () {
        animator.speed = Mathf.Abs(rb.angularVelocity * 0.002f);
	}
}
