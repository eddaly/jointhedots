using UnityEngine;
using System.Collections;

public class Hud : MonoBehaviour {
	
	// Hud's child objects
	GameObject scoreCounter;

	// Use this for initialization
	void Start () {
		
		scoreCounter = transform.FindChild("ScoreCounter").gameObject;
		scoreCounter.guiText.material.color = Color.blue;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// Called by PlayField.Update 
	public void SetScore (int score)
	{
		scoreCounter.guiText.text = "Score " + score; 
	}
}
