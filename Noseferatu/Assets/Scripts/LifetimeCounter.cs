using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Destroys and object after X seconds
/// </summary>
public class LifetimeCounter : MonoBehaviour {

    public float time;

	// Use this for initialization
    IEnumerator Start () {
        yield return new WaitForSeconds (time);
        Destroy (gameObject);
	}
	
}
