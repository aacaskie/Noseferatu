using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

public class SoundManager : Singleton<SoundManager> {
	
    public AudioBank bank;

    private AudioSource _source;
    public AudioSource source {
        get{ return _source ?? (_source = GetComponentInChildren<AudioSource> ()); }
    }

    public void PlaySound(string name){
        AudioClip s = bank.Sounds.Single (x => x.Name == name).Clip;

        source.PlayOneShot (s);
    }

}
