using UnityEngine;
using System.Collections;

public class MenuMotion : MonoBehaviour {
	float frequency;
	float magnitude;
	private Vector3 pos;
	float wave;
	// Use this for initialization
	void Start () {
		pos = transform.localPosition;
		frequency = Random.Range (8.0f, 12.0f);
		magnitude = Random.Range (0.05f, 0.07f);
	}
	
	// Update is called once per frame
	void Update () {
		wave = (Mathf.Cos (Time.time * frequency) * magnitude);
		transform.position = new Vector3 (pos.x, pos.y+ wave, pos.z);
	}
}
