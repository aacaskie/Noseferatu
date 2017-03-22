using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Collection of audio clips to be chosen from
/// </summary>
[CreateAssetMenu(fileName = "AudioCollection", menuName = "Audio/Audio Collection", order = 1)]
public class AudioCollection : ScriptableObject {
    
    public List<AudioClip> Clips;

}