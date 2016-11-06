using UnityEngine;
using System.Collections;

public class backgroundScroll : MonoBehaviour {

    private MeshRenderer _renderer;
	public float speed;

    public MeshRenderer renderer {
        get{ return _renderer ?? (_renderer = GetComponentInChildren<MeshRenderer> ()); }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        renderer.material.SetTextureOffset ("_MainTex", new Vector2 (Time.time * 0.12f * speed, 0));
	}
}
