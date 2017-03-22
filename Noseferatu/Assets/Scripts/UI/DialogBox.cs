using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBox : MonoBehaviour {

    private TextWriter _textWriter;
    public TextWriter textWriter
    {
        get{ return _textWriter ?? (_textWriter = GetComponentInChildren<TextWriter>()); }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator Exit(){
        iTween.MoveBy (textWriter.gameObject, iTween.Hash(
            "ignoreTimescale", true,
            "amount", Vector3.down * 10,
            "time", 1.0f
        ));
        yield return new WaitForSecondsRealtime (1.0f);
        Destroy (gameObject);
    }
}
