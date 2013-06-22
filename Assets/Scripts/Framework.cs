using UnityEngine;
using System.Collections;

public class Framework : MonoBehaviour {
	
	// Hook ups done in editor
	public Hud hud;
	
	// Accessable variables not available in editor
	[HideInInspector]
	public int score;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Escape))
		{
			Application.LoadLevel (0);
		}
	}
	
	// Update HUD
	void OnGUI () {
	
		hud.SetScore (score);
		if (PersistentData.hiScore < score)
			PersistentData.hiScore = score;
	}
}
