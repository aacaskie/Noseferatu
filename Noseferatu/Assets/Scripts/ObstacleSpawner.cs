using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour {

    public GameObject obsPrefab;

	// Use this for initialization
	void Start () {
        InvokeRepeating ("Spawn", 2, 2.5f);
        InvokeRepeating ("Spawn", 2, 2.5f);

        InvokeRepeating ("Spawn", 1, 2.5f);

        InvokeRepeating ("Spawn", 1.5f, 1.5f);

        InvokeRepeating ("Spawn", 1.3f, 2.2f);
        InvokeRepeating ("Spawn", 1.2f, 2.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Spawn(){
        float n = Camera.main.orthographicSize * 0.9f;
        Instantiate (obsPrefab, transform.position + Vector3.up * Random.Range (-n, n), Quaternion.identity);
    }
}
