using UnityEngine;
using System.Collections;

public class Hud : MonoBehaviour {
	
	// Hud's child objects
	GameObject scoreCounter, exit;

	// Use this for initialization
	void Start () {
		
		scoreCounter = transform.FindChild("ScoreCounter").gameObject;
		scoreCounter.guiText.material.color = Color.blue;
		exit = transform.FindChild("Exit").gameObject;
		exit.guiText.material.color = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
		
		// 'Button' hittests and responses
		if (Input.GetMouseButtonUp (0))
		{
			// Back to previous screen
			if (exit.guiText.HitTest (Input.mousePosition))
			{
				Application.LoadLevel ("startup");
			}
		}
	}
	
	// Called by PlayField.Update 
	public void SetScore (int score)
	{
		scoreCounter.guiText.text = "Score " + score; 
	}
}
