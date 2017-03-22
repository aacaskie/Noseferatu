using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class DialogManager : Singleton<DialogManager> {

    public Canvas canvas;

    public DialogBox DialogBoxPrefab;

    public void StartDialog(string message){
        StartCoroutine (DialogRoutine (new string[]{ message }));
    }

    public void StartDialog(string[] messages){
        StartCoroutine (DialogRoutine (messages));
    }

    IEnumerator DialogRoutine(string[] messages){

        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime (0.5f);

        //now spawn a dialog box, and pass messages into it one at a time
        var dialogBox = Instantiate(DialogBoxPrefab);

        dialogBox.transform.SetParent (canvas.transform, false);

        foreach (string message in messages) {

            dialogBox.textWriter.WriteText (message);

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
    }
}
