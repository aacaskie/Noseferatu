using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioCollection", menuName = "Audio/Audio Collection", order = 1)]
public class AudioCollection : ScriptableObject {
    
    public List<AudioClip> Clips;

}