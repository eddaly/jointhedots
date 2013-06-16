using UnityEngine;
using System.Collections;

public class Hud : MonoBehaviour {
	
	// Hud's child objects
	GameObject chainLength;

	// Use this for initialization
	void Start () {
		
		chainLength = transform.FindChild("ChainLength").gameObject;
		chainLength.guiText.material.color = Color.blue;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// Called by PlayField.Update 
	public void SetChainLength (int len)
	{
		chainLength.guiText.text = "Chain Length " + len; 
	}
}
