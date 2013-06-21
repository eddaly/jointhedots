using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayField : MonoBehaviour {
	
	// Hook ups done in editor
	public Framework framework;
	public Dot m_DotPrefab;

	// Configure number of dots and scroll speed
	public int m_DotsAcross = 10;
	public float m_ScrollSpeed = 1;
	public float m_DotScale = 1;
	
	// The dots array
	Dot[,] dots;
	int dotsDown;
	//int dotsRecycleY;
	
	// Dots position and playfield position
	float dotSize, dotGap;
	float xOrigin, yOrigin;
	
	// The 'chain'
	List <Dot> chain;
	Color chainColour;
	
	// Sound FX
	public AudioSource m_Click, m_Stamp;
	
	// State
	enum State {MAKINGCHAIN = 0, CHAINCOMPLETE, DOTSFALLING};
	State state;
	float dotsFallingAnim_PauseDuration = .5f;
	float dotsFallingAnimDuration = 1;
	float dotsFallingTimer;
	
	// Use this for initialization
	void Start () {
		
		Dot.ClassSetup ();
		
		// Adjust scale for more or less dots (I set-up with 6)
		m_DotScale *= 6f/m_DotsAcross;
		
		// Set up the dots across the PlayField

		dotsDown = 3;//m_DotsAcross * Screen.currentResolution.width / Screen.currentResolution.height /2;//*2 // Plenty enough room to cover the screen
		dots = new Dot [m_DotsAcross, dotsDown];
		dotSize = m_DotScale * m_DotPrefab.transform.localScale.z * 10; // Unity Planes are x10 units
		dotGap = dotSize / 20;
		xOrigin = -((m_DotsAcross-1) * (dotSize + dotGap)) / 2;
		yOrigin = 0;//+(m_DotsDown   * (size + gap)) / 2;
		
		for (int x = 0; x < m_DotsAcross; x++)
		{
			for (int y = 0; y < dotsDown; y++)
			{
				dots[x,y] = Instantiate (m_DotPrefab, new Vector3 (
					xOrigin + +x * (dotSize + dotGap),
					-1, 
					yOrigin + -y * (dotSize + dotGap)), 
					Quaternion.identity) as Dot;
				dots[x,y].transform.localScale *= m_DotScale;
				dots[x,y].transform.parent = this.transform;
				dots[x,y].playField = this;
				dots[x,y].xPos = x;
				dots[x,y].yPos = y;
				dots[x,y].Setup ();
			}
		}
		
		// Last row needs a CLEARALLCOLOUR in each colour to enable perfect completion
		int r = Random.Range (0,m_DotsAcross);
		dots[r,dotsDown - 1].SetPower (Dot.Power.CLEARALLCOLOUR);
		dots[r,dotsDown - 1].SetColour (Dot.red);
		int r2;
		do {r2 = Random.Range (0,m_DotsAcross);} while (r2 == r);
		dots[r2,dotsDown - 1].SetPower (Dot.Power.CLEARALLCOLOUR);
		dots[r2,dotsDown - 1].SetColour (Dot.green);
		int r3;
		do {r3 = Random.Range (0,m_DotsAcross);} while (r3 == r || r3 == r2);
		dots[r3,dotsDown - 1].SetPower (Dot.Power.CLEARALLCOLOUR);
		dots[r3,dotsDown - 1].SetColour (Dot.blue);		
			
		//dotsRecycleY = 0;
		
		// Initialise the chain list
		chain = new List<Dot>();
		state = State.MAKINGCHAIN;
	}
	
	// Update the hud
	void OnGUI ()
	{
		//hud.SetChainLength (chain.Count);
	}
	
	// Update is called once per frame
	void Update () {
		
		// Scroll the PlayField up
		Vector3 pos = transform.position;
		pos.z += Time.deltaTime * m_ScrollSpeed;
		transform.position = pos;
		
		switch (state)
		{
		case State.MAKINGCHAIN:
#if UNITY_IPHONE || UNITY_ANDROID
			if (chain.Count > 0)
			{
				if (Input.touchCount == 1)
				{
					if (Input.GetTouch(0).phase == TouchPhase.Ended)
					{
						CompleteChain ();
					}
				}
			}
#endif
			/*if (false) //*** If row just went off screen
			 This puts a new row from the top (will be cleared by deathline at least)
			 but then need to make the bottom wrap around to the top
			 or need to move them all up one - painful either way... 
			{
				// Recycle dots
				int y = dotsRecycleY;
				for (int x = 0; x < m_DotsAcross; x++)
				{
					if (dots[x,y].gameObject != null)
					{
						Debug.LogError ("Error, this should have been destroyed");
						DestroyObject (dots[x,y].gameObject);
					}
					dots[x,y] = Instantiate (m_DotPrefab, new Vector3 (
						xOrigin + +x * (dotSize + dotGap),
						-1, 
						yOrigin + -y * (dotSize + dotGap)), 
						Quaternion.identity) as Dot;
					dots[x,y].transform.localScale *= m_DotScale;
					dots[x,y].transform.parent = this.transform;
					dots[x,y].playField = this;
					dots[x,y].xPos = x;
					dots[x,y].yPos = y;
				}
				if (++dotsRecycleY >= dotsDown)
					dotsRecycleY = 0;
			}*/
			break;
			
		case State.CHAINCOMPLETE:

			// Then destroy all the chain
			foreach (Dot dot in chain)
			{
				DestroyObject (dot.gameObject);//*** getting null ref exception in android and have seen floating dots in android and webplayer too
			}
			
			// And do dots falling
			dotsFallingTimer = 0;
			state = State.DOTSFALLING;
			break;
		
		case State.DOTSFALLING:
				
			// If dots still falling
			dotsFallingTimer += Time.deltaTime;
			if (dotsFallingTimer < dotsFallingAnimDuration)
			{
				// Unless during the initial pause
				if (dotsFallingTimer > dotsFallingAnim_PauseDuration)
				{
					// Scan across playfield
					for (int x = 0; x < m_DotsAcross; x++)
					{
						// Find the lowest and highest y positions in chain, if there is one on this column
						int minypos = dotsDown, maxypos = 0;
						foreach (Dot dot in chain)
						{
							if (dot.xPos == x)
							{
								if (dot.yPos < minypos)
									minypos = dot.yPos;
								if (dot.yPos > maxypos)
									maxypos = dot.yPos;
							}
						}
						// If the chain is in this column and it's not on the top row
						if (minypos != dotsDown && minypos > 0) //*** Think OK on last row?
						{
							int ydrop = (maxypos - minypos) + 1;
							
							// Scan up the column above the highest dot in chain
							for (int y = maxypos; y >= ydrop; y--) 
							{
								if (dots[x,y-ydrop] == null)	// No dots left
									break;
							
								dots[x,y] = dots[x,y-ydrop];	// Drop down in the array
								dots[x,y-ydrop] = null;
								dots[x,y].xPos = x;				// Update the references
								dots[x,y].yPos = y;
								Vector3 dotpos = new Vector3 (	// And move the transform
									xOrigin + +x * (dotSize + dotGap), -1, yOrigin + -y * (dotSize + dotGap));
								dots[x,y].transform.localPosition = dotpos;
							}
						}
					}
					dotsFallingTimer = dotsFallingAnimDuration;  // We're done
				}
			}
			else
			{
				// Now can clear the chain and start again
				chain.Clear ();
				state = State.MAKINGCHAIN;
			}
			break;
		}			
	}
	
	// Called when first touch a dot
	public void NewChain (Dot dot)
	{
		// If still dealing with last chain, need to wait
		if (state != State.MAKINGCHAIN)
			return;
		
		chainColour = dot.GetColour ();
		chain.Add (dot);
		dot.SetHighlight (true);
		m_Click.Play ();
	}
	
	public void ChainAddDot (Dot dot)
	{
		// If still dealing with last chain, need to wait
		if (state != State.MAKINGCHAIN)
			return;

		// If still adding dots (because still touching) but the chain has been cleared, skip
		if (chain.Count == 0)
			return;
		
		// If touching same dot as last frame, skip
		if (dot == chain[chain.Count - 1])
			return;
		
		// If new dot is different colour, reset
		if (dot.GetColour() != chainColour)
		{
			CompleteChain ();
			return;
		}
		
		// If new dot is not 1 away on horizontal or vertical line, reset
		float xdif = Mathf.Abs (dot.xPos - chain[chain.Count - 1].xPos);
		float ydif = Mathf.Abs (dot.yPos - chain[chain.Count - 1].yPos);
		if ((xdif > 1 || ydif > 1) || (xdif == 1 && ydif == 1)) 
		{
			CompleteChain ();
			return;
		}
		
		// If doubling back on existing chain, reset
		for (int n = 0; n < chain.Count - 1; n++)
		{
			if (chain[n] == dot)
			{
				CompleteChain ();
				return;
			}
		}
				
		// Otherwise extend the chain
		chain.Add (dot);
		dot.SetHighlight (true);
		m_Click.Play ();
	}
	
	// Completed chain, note also called when touch ended
	public void CompleteChain ()
	{
		if (chain.Count == 1) // Don't allow singles, may be zero (if this called twice, once due to colour miss then again as mouse/touch ended)
		{
			chain[0].SetHighlight (false);
			chain.Clear ();
		}
		else
		{
			state = State.CHAINCOMPLETE;
			m_Stamp.Play ();
			framework.score += chain.Count * 10;
		}
	}
}

/*
 
 recycle for infinite game or just have some autowipe out effect at end? special dots that wipe out all colours on screen and have
 them on the bottom row
 
 note using mesh for dots is 100x too many triangles (unless going to distort them cooly)
 
 fragments that escape lose lives/points
 speed increases over time?
 how animate? need a programmatic animation system easy to use (fire and forget)
 sfx
 HUD
 FE - load/save
 icons and whatnot
 ios warnings
 proper release builds
 music? (itunes referral?)
 tutorial has bespoke data that stops to tell you what to do like candy crush
 game centre
 ads - pay to remove
 iaps
 flurry
 publish
 fb ad
 google adwords
 admob
 try trademob
 sell on apptopia!
 */
 