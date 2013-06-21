using UnityEngine;
using System.Collections;

public class Dot : MonoBehaviour {
	
	// Accessable variables not available in editor
	[HideInInspector]
	public PlayField playField;
	[HideInInspector]
	public int xPos, yPos;

	// Editor parameters
	public float m_DotInSquareSize = .75f;	// Size scaler
	public float m_PowerClearAllColourProbability = 0.1f;
	
	// Private variables
	GameObject texture;				// Child object (how I'm allowing a smaller dot than the collision box it's in)
	public enum Power {NONE = 0, CLEARALLCOLOUR};
	Power power;
	Color colour = Color.black;				// Note, using this as a flag for unset
	public static Color red, green, blue;	// Default colours
	

	// Setup the class
	public static void ClassSetup ()
	{
		Random.seed = 0;
		red = new Color (.75f, 0, 0, 1);
		green = new Color (0, .75f, 0, 1);
		blue = new Color (0, 0, .75f, 1);
		
	}
	
	// Playfield needd to call this as Start() is called too late 
	public void Setup ()
	{
		// Randomise dot colour
		int r = Random.Range (0, 3);
		if (r == 0)
			colour = red;
		else if (r == 1)
			colour = green;
		else
			colour = blue;
		texture = transform.FindChild("Texture").gameObject;
		texture.renderer.material.color = colour;
		
		float rf = Random.Range (0f,1f);
		if (rf < m_PowerClearAllColourProbability) {
			power = Power.CLEARALLCOLOUR;
			texture.renderer.material.shader = Shader.Find ("Self-Illumin/Diffuse");
		}
		else {
			power = Power.NONE;
		}
		
		// Scaling the child texture to allow small dot in full sized collidable square
		texture.transform.localScale *= m_DotInSquareSize; 
	}
	
	// Use this for initialization
	void Start () {
			
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
				texture.renderer.material.color = new Color (1, .5f, 0, 1);
			else if (colour == green)
				texture.renderer.material.color = new Color (.5f, 1, 0, 1);
			else
				texture.renderer.material.color = new Color (0, .5f, 1, 1);
		}
		else
		{
			texture.renderer.material.color = colour;
		}
	}
	
	// Set the dot's power rather than rely on randomised, used for end of PlayField
	public void SetPower (Power newPower)
	{
		power = newPower;
		if (power == Power.CLEARALLCOLOUR) {
			texture.renderer.material.shader = Shader.Find ("Self-Illumin/Diffuse");
		}
	}
	
	// Set the dot's colour
	public void SetColour (Color newColour) {
		colour = newColour;
		texture.renderer.material.color = colour;
	}
	
	// Set the dot's colour
	public Color GetColour () {
		return colour;
	}

	// Message sent by DeadLine.OnTriggerEnter
	void HitDeathLine () {
		DestroyObject (gameObject);
	}
}
