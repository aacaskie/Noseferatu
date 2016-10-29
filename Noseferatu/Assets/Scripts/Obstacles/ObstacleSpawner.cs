using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ObstacleSpawner : MonoBehaviour {

    public List<Action> actions;

    private float WaitTime = 2f;

	// Use this for initialization
	void Awake () {
        UnityEngine.Random.seed = 1238;
        actions = new List<Action> ();
        StartCoroutine ("StartWave");
	}

    public void AddAction(Action a){
        actions.Add (() => a());
    }

    IEnumerator StartWave(){
        Debug.Log ("starting wave");
        yield return new WaitForSeconds(2f);
        foreach (Action a in actions) {
            a ();
            yield return new WaitForSeconds(WaitTime);
        }
    }


    public void SetTime(float n){
        WaitTime = n;
    }

    public void AddSetTime(float n){
        AddAction (() => SetTime (n));
    }

    public void Spawn(GameObject prefab){
        
        float n = Camera.main.orthographicSize * 0.9f;
        //TODO: Make the obstacles spawn in correct spot
        Instantiate (
            prefab, 
            Camera.main.transform.position + transform.position + Vector3.up * UnityEngine.Random.Range (-n, n), 
            Quaternion.identity
        );

    }

    public void AddSpawn(GameObject prefab){
        AddAction(() => Spawn(prefab));
    }

    public void AddSpawn(GameObject prefab, int amount, float inbetween = 0.1f, float wait = 2f){

        AddAction(() => SetTime (inbetween));

        for (int i = 0; i < amount; i++) {
            AddAction(() => Spawn(prefab));
        }

        AddAction(() => SetTime (wait));

    }
}
