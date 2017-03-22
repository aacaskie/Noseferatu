using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Writes text into a textbox
using UnityEngine.UI;
using System.Text;


public class TextWriter : MonoBehaviour {

    private Text _textbox;

    public Text textbox {
        get{ return _textbox ?? (_textbox = GetComponentInChildren<Text> ()); }
    }

    public string message;
    public bool isFinished = false;

    public void WriteText(string words){
        isFinished = false;
        textbox.text = string.Empty;
        message = words;
        StartCoroutine(writeText(message));
    }
    
    private IEnumerator writeText(string words) {

        yield return new WaitForSecondsRealtime(0.2f);

        StringBuilder sb = new StringBuilder();

        foreach(char c in words) {
            if (isFinished)
                break;
            sb.Append(c);
            textbox.text = sb.ToString();
            yield return new WaitForSecondsRealtime(0.02f);
        }

        isFinished = true;
    }

    public void Finish(){
        StopCoroutine ("writeText");
        print ("finishing");
        textbox.text = message;
        isFinished = true;
    }

}
