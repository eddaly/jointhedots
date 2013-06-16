using UnityEngine;
using System.Collections;

public class DeathLine : MonoBehaviour {
	
	public Framework framework;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter (Collider dotCollider) //*** SHouldn't have had to but ended up making Dot a kinetic rigidbody to get this to trigger
	{
		dotCollider.SendMessage ("HitDeathLine");
		framework.score -= 100;
		if (framework.score < 0)
			framework.score = 0;
	}
}
