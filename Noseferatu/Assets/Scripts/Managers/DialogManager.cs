using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class DialogManager : Singleton<DialogManager> {

    public struct Message
    {
        public Sprite sprite;
        public string message;

        public Message(string message, Sprite sprite){
            this.sprite = sprite;
            this.message = message;
        }
    }

    public bool Busy = false;

    public Canvas canvas;

    public DialogBox DialogBoxPrefab;


    public void StartDialog(List<Message> messages){
        StartCoroutine (DialogRoutine ( messages ));
    }

    public void StartDialog(string[] strings){
       
        List<Message> messages = new List<Message>();

        foreach (string s in strings) {
            messages.Add (new Message (s, null));
        }

        StartCoroutine (DialogRoutine (messages));
    }

    public void StartDialog(string message){
        StartCoroutine (DialogRoutine (new List<Message>(){ new Message(message, null) }));
    }

    public void StartDialog(Message message){
        StartCoroutine (DialogRoutine (new List<Message>(){ message }));
    }


    IEnumerator DialogRoutine(List<Message> messages){

        Time.timeScale = 0;
        Busy = true;

        yield return new WaitForSecondsRealtime (0.5f);

        //now spawn a dialog box, and pass messages into it one at a time
        var dialogBox = Instantiate(DialogBoxPrefab);
        dialogBox.transform.SetParent (canvas.transform, false);

        foreach (Message message in messages) {

            //Assign the sprite if it's workin'
            if (message.sprite == null)
                dialogBox.FaceImage.enabled = false;
            else {
                dialogBox.FaceImage.enabled = true;
                dialogBox.FaceImage.sprite = message.sprite;
            }

            dialogBox.textWriter.WriteText (message.message);

            while (!Input.GetButtonDown ("Fire") && !dialogBox.textWriter.isFinished) {
                yield return null;
            }

            dialogBox.textWriter.Finish();

            yield return new WaitForSecondsRealtime (0.2f);

            while (!Input.GetButtonDown ("Fire")) {
                yield return null;
            }

            //destroy the animate the item out

            yield return new WaitForSecondsRealtime (0.02f);

        }

        yield return StartCoroutine(dialogBox.Exit());

        yield return new WaitForSecondsRealtime (0.1f);

       
        Time.timeScale = 1;
        Busy = false;
    }
}
