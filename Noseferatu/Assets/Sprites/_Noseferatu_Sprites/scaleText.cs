using UnityEngine;
using System.Collections;

public class scaleText : MonoBehaviour {
	Vector3 textScale = new Vector3(1.1f,1.1f,20f);
	Vector3 posScale;
	void Start(){
		posScale = transform.localScale;
	}
	public void ScaleText(){
		transform.localScale = new Vector3 (posScale.x * textScale.x,posScale.y * textScale.y, posScale.z); 

	}
	public void UnScaleText(){
		transform.localScale = posScale;
	}
	void OnMouseOver(){
		ScaleText ();
	}
	void OnMouseExit(){
		UnScaleText ();
	}
}
