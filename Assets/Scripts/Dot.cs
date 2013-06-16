using UnityEngine;
using System.Collections;

public class Dot : MonoBehaviour {
	
	// Accessable variables not available in editor
	[HideInInspector]
	public Color colour;
	[HideInInspector]
	public PlayField playField;
	[HideInInspector]
	public int xPos, yPos;

	// Size scaler
	public float m_DotInSquareSize = .75f;
	
	GameObject texture;
	
	// Default colours
	static Color red, green, blue;
	
	static bool first = true;

	// Use this for initialization
	void Start () {
	
		// Randomise dot colour
		if (first)	
		{
			Random.seed = 0;
			first = false;
			red = new Color (.75f, 0, 0, 0);
			green = new Color (0, .75f, 0, 0);
			blue = new Color (0, 0, .75f, 0);
		}
		int r = Random.Range (0, 3);
		if (r == 0)
			colour = red;
		else if (r == 1)
			colour = green;
		else
			colour = blue;
		
		texture = transform.FindChild("Texture").gameObject;
		texture.renderer.material.color = colour;
		texture.transform.localScale *= m_DotInSquareSize;
	}
	
	// Update is called once per frame
	void Update ()
	{
#if UNITY_IPHONE || UNITY_ANDROID
		if (Input.touchCount == 1)
		{
			if (Input.GetTouch(0).phase == TouchPhase.Began)
			{
				Ray ray = Camera.main.ScreenPointToRay (Input.GetTouch(0).position);
				RaycastHit hit;
				if (collider && collider.Raycast (ray, out hit, 100.0f))
				{
					playField.NewChain (this);
				}
			}
			else if (Input.GetTouch(0).phase == TouchPhase.Moved)
			{
				Ray ray = Camera.main.ScreenPointToRay (Input.GetTouch(0).position);
				RaycastHit hit;
				if (collider && collider.Raycast (ray, out hit, 100.0f))
				{
					playField.ChainAddDot (this);
				}
			}
		}
#endif
	}
	
#if UNITY_WEBPLAYER
	
	// ***Note for iPhone (and maybe Android?) need to use Input.touches instead
	void OnMouseDown ()
	{
		playField.NewChain (this);
	}
	
	void OnMouseOver ()
	{
		if (Input.GetMouseButton (0))
		{
			playField.ChainAddDot (this);
		}
	}
	
	void OnMouseUp ()
	{
		playField.CompleteChain ();
	}
	
#endif
	
	// PlayField set's whether or no in a snake
	public void SetHighlight (bool isHighlighted)
	{
		if (isHighlighted)
		{
			if (colour == red)
				texture.renderer.material.color = new Color (1, .5f, 0, 0);
			else if (colour == green)
				texture.renderer.material.color = new Color (.5f, 1, 0, 0);
			else
				texture.renderer.material.color = new Color (0, .5f, 1, 0);
		}
		else
		{
			texture.renderer.material.color = colour;
		}
	}
	
	void HitDeathLine ()
	{
		DestroyObject (gameObject);
	}
}
