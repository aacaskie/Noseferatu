using UnityEngine;
using System.Collections;
using System;

public class Wave1 : ObstacleSpawner {

    public GameObject knifePrefab;
    public GameObject garlicPrefab;

	// Use this for initialization
	void Start () {
        
        for (int i = 0; i < 5; i++) {
            AddSpawn (garlicPrefab, 3, 0.1f, 0.2f);
            AddSpawn (knifePrefab, 3, 0.2f, 2f);
            AddSpawn (knifePrefab, 2, 0.1f, 1.0f);
            AddSpawn (garlicPrefab, 3, 0.1f, 0.2f);
            AddSpawn (knifePrefab, 5, 0.05f, 3f);

            AddSpawn (knifePrefab, 9, 0.3f, 1.0f); 
            AddSpawn (garlicPrefab, 3, 0.1f, 1.2f);
        }

        for (int i = 0; i < 5; i++) {
            AddSpawn (knifePrefab, 6, 0.1f, 2f);
            AddSpawn (knifePrefab, 6, 0.1f, 0.7f);
        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
