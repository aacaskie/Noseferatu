using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UiBehaviorScript : MonoBehaviour {
	public Button Startbutton;
	public Button LoadButton;
	public GameObject MenuNose;

	private IEnumerator BeginLevel(float delay)
	{
		MenuNose.GetComponent<menuNose> ().OnTheMove ();
		yield return new WaitForSeconds (delay);
		Application.LoadLevel (1);
	}
	public void StartButton(){
		Debug.Log ("great");
		StartCoroutine (BeginLevel (2.0f));

	}

}
