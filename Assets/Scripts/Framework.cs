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
	
	}
	
	// Update HUD
	void OnGUI () {
	
		hud.SetScore (score);
	}
}
