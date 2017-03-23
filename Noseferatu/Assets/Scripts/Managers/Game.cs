using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

/// <summary>
/// This is the main object for controlling the game. 
/// Any universal or global objects/vars will be stored in this singleton
/// </summary>
public class Game : Singleton<Game> {

    public PlayerInfo PlayerInfo;
    
	// Use this for initialization
	void Awake () {
        Object.DontDestroyOnLoad (gameObject); //Keep me, level to level

        PlayerInfo = new PlayerInfo ();
        PlayerInfo.Health = 2;
        PlayerInfo.MaxHealth = 2;
	}

	// Update is called once per frame
	void Update () {
		
	}

    public void SlowTime(float time){
        StartCoroutine ("AlterTime");
    }

    IEnumerator AlterTime(){
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime (0.14f);
        //if DialogManager.instance...
        //don't reset timescale if game is paused due to pause screen or dialog thing?...
        Time.timeScale = 1;
    }
}

[System.Serializable]
public class PlayerInfo{

    public float Health;
    public float MaxHealth;

    public bool isDead{
        get { return Health <= 0;}
    }

    public void TakeDamage(float amount){
        Health -= amount;

        //Check for death

        iTween.ShakePosition (Camera.main.gameObject, iTween.Hash(
            "Amount", Vector3.one * 0.3f,
            "time", 0.6f,
            "ignoreTimescale", true
        ));
    }

}

