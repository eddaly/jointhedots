using UnityEngine;
using System.Collections;

public class Startup : MonoBehaviour {
	
	GameObject playGame, persistentData;

	// Use this for initialization
	void Start () {
		playGame = transform.FindChild("PlayGame").gameObject;
		persistentData = transform.FindChild("PersistentData").gameObject;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonUp (0))
		{
			if (playGame.guiText.HitTest (Input.mousePosition))
			{
				Application.LoadLevel ("scene1");
			}
			else if (persistentData.guiText.HitTest (Input.mousePosition))
			{
				Application.LoadLevel ("persistentdata");
			}
		}
	}
}
