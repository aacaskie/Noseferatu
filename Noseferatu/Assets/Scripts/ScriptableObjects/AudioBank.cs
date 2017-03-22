using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioBank", menuName = "Audio/Audio Bank", order = 1)]
public class AudioBank : ScriptableObject {
    
    public List<Sound> Sounds;

}

[System.Serializable] 
public class Sound{
    public string Name;
    public AudioCollection Collection;
}