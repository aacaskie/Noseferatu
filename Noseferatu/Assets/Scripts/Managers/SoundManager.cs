using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

/// <summary>
/// Singleton for playing sounds during the game
/// </summary>
public class SoundManager : Singleton<SoundManager> {
	
    public AudioBank bank;

    private AudioSource _source;
    public AudioSource source {
        get{ return _source ?? (_source = GetComponentInChildren<AudioSource> ()); }
    }

    public void PlaySound(string name, float volume = 1f){

		//Get random sound from this collection
		var collection = bank.Sounds.Single (x => x.Name == name).Collection.Clips;
		int r = Random.Range(0,collection.Count());
		AudioClip s = collection[r];

		//and play it!
        source.PlayOneShot (s, volume);
    }

}
