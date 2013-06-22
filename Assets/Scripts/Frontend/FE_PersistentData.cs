using UnityEngine;
using System.Collections;

public class FE_PersistentData : MonoBehaviour {

	// These are the child GUIText objects
	GameObject back, reset, cashSpentGBP, highScore, xp, winXP, coins, winCoins, goldDiscs, goldDiscsBuyCoins, goldDiscsBuyCash, jumpsuit, jumpsuitUpgrade;
	GameObject blueSuedeShoes, blueSuedeShoesBuy, armyGun, armyGunBuy, speedwayHelmet, speedwayHelmetBuy;

	// Use this for initialization
	void Start () {
		
		// Hook up the child GUI objects (note will throw exception on null FindChild if missing / naming error
		back = transform.FindChild("Back").gameObject;
		reset = transform.FindChild("Reset").gameObject;
		cashSpentGBP = transform.FindChild("CashSpentGBP").gameObject;
		highScore = transform.FindChild("HighScore").gameObject;
		xp = transform.FindChild("XP").gameObject;
		winXP = transform.FindChild("WinXP").gameObject;
		coins = transform.FindChild("Coins").gameObject;
		winCoins = transform.FindChild("WinCoins").gameObject;
		goldDiscs = transform.FindChild("GoldDiscs").gameObject;
		goldDiscsBuyCoins = transform.FindChild("GoldDiscsBuyCoins").gameObject;
		goldDiscsBuyCash = transform.FindChild("GoldDiscsBuyCash").gameObject;
		jumpsuit = transform.FindChild("Jumpsuit").gameObject;
		jumpsuitUpgrade = transform.FindChild("JumpsuitUpgrade").gameObject;
		blueSuedeShoes = transform.FindChild("BlueSuedeShoes").gameObject;
		blueSuedeShoesBuy = transform.FindChild("BlueSuedeShoesBuy").gameObject;
		armyGun = transform.FindChild("ArmyGun").gameObject;
		armyGunBuy = transform.FindChild("ArmyGunBuy").gameObject;
		speedwayHelmet = transform.FindChild("SpeedwayHelmet").gameObject;
		speedwayHelmetBuy = transform.FindChild("SpeedwayHelmetBuy").gameObject;				
	}
	
	// Update the GUI objects content
	void UpdateGUITexts ()
	{
		// Debug only, will not display!
		cashSpentGBP.guiText.text = "Cash Spent (GBP) " + PersistentData.cashSpentGBP;
		
		// The simple stuff
		highScore.guiText.text = "High Score " + PersistentData.hiScore;
		xp.guiText.text = "XP " + PersistentData.xP + " Level: " + PersistentData.CurrentLevel();
		coins.guiText.text = "Coins " + PersistentData.coins;
		goldDiscs.guiText.text = "Gold Discs " + PersistentData.goldDiscs;
		
		// Buy GDs include price
		goldDiscsBuyCoins.guiText.text = "Buy with Coins (" + PersistentData.instance.m_GoldDiscsPriceCoins + ")";
		goldDiscsBuyCash.guiText.text = "Buy with Cash (" + PersistentData.instance.m_GoldDiscsPriceCashGBP + "GBP)";
		
		// If don't have this upgrade
		if (PersistentData.upgrades == PersistentData.Upgrades.JUMPSUIT_NONE)
		{
			// Set the text
			jumpsuit.guiText.text = "Jumpsuit NONE";
	
			// If Locked, update display accordingly
			if (PersistentData.CurrentLevel() < PersistentData.instance.m_JumpsuitRedUnlockLevel)
			{
				jumpsuitUpgrade.guiText.text = "Locked (" + PersistentData.instance.m_JumpsuitRedUnlockLevel +")";
				jumpsuitUpgrade.guiText.material.color = Color.grey;
			}
			else
			{
				jumpsuitUpgrade.guiText.material.color = Color.white;
				jumpsuitUpgrade.guiText.text = "Upgrade (" + PersistentData.instance.m_JumpsuitRedPriceCoins + " Coins" +
											" & "+ PersistentData.instance.m_JumpsuitRedPriceGoldDiscs + " GD)";
			}
		}
		// Else if have the upgrade
		else if (PersistentData.upgrades == PersistentData.Upgrades.JUMPSUIT_RED)
		{
			jumpsuit.guiText.text = "Jumpsuit RED";
			
			// If Locked, update display accordingly
			if (PersistentData.CurrentLevel() < PersistentData.instance.m_JumpsuitSilverUnlockLevel)
			{
				jumpsuitUpgrade.guiText.text = "Locked (" + PersistentData.instance.m_JumpsuitSilverUnlockLevel +")";
				jumpsuitUpgrade.guiText.material.color = Color.grey;
			}
			else
			{
				jumpsuitUpgrade.guiText.material.color = Color.white;
				jumpsuitUpgrade.guiText.text = "Upgrade (" + PersistentData.instance.m_JumpsuitSilverPriceCoins + " Coins" +
											" & "+ PersistentData.instance.m_JumpsuitSilverPriceGoldDiscs + " GD)";
			}
		}
		// Else if have the upgrade 
		else if (PersistentData.upgrades == PersistentData.Upgrades.JUMPSUIT_SILVER)
		{
			jumpsuit.guiText.text = "Jumpsuit SILVER";
			
			// If Locked, update display accordingly
			if (PersistentData.CurrentLevel() < PersistentData.instance.m_JumpsuitGoldUnlockLevel)
			{
				jumpsuitUpgrade.guiText.text = "Locked (" + PersistentData.instance.m_JumpsuitGoldUnlockLevel +")";
				jumpsuitUpgrade.guiText.material.color = Color.grey;
			}
			else
			{
				jumpsuitUpgrade.guiText.material.color = Color.white;
				jumpsuitUpgrade.guiText.text = "Upgrade (" + PersistentData.instance.m_JumpsuitGoldPriceCoins + " Coins" +
											" & "+ PersistentData.instance.m_JumpsuitGoldPriceGoldDiscs + " GD)";
			}
		}
		// Else if have the upgrade
		else// if (PersistentData.upgrades == PersistentData.Upgrades.JUMPSUIT_GOLD)
		{
			jumpsuit.guiText.text = "Jumpsuit GOLD";
			jumpsuitUpgrade.guiText.material.color = Color.grey;
		}
		
		// Unless have the item
		if (!PersistentData.HasItem (PersistentData.Items.BLUESUEDESHOES))
		{
			blueSuedeShoes.guiText.text = "Blue Suede Shoes ";
			
			// If Locked, update display accordingly
			if (PersistentData.CurrentLevel() < PersistentData.instance.m_BlueSuedeShoesUnlockLevel)
			{
				blueSuedeShoes.guiText.text += "Locked (" + PersistentData.instance.m_BlueSuedeShoesUnlockLevel +")";
				blueSuedeShoesBuy.guiText.material.color = Color.grey;
			}
			else
				blueSuedeShoesBuy.guiText.material.color = Color.white;
			blueSuedeShoes.guiText.material.color = Color.grey;
			blueSuedeShoesBuy.guiText.text = "Buy (" + PersistentData.instance.m_BlueSuedeShoesPriceCoins + " Coins" +
											" & "+ PersistentData.instance.m_BlueSuedeShoesPriceGoldDiscs + " GD)";
		}
		// Have it already, update display
		else
		{
			blueSuedeShoes.guiText.material.color = Color.white;			
			blueSuedeShoesBuy.guiText.material.color = Color.grey;
		}
		
		// Unless have the item
		if (!PersistentData.HasItem (PersistentData.Items.ARMYGUN))
		{
			armyGun.guiText.text = "Army Gun ";
			
			// If Locked, update display accordingly
			if (PersistentData.CurrentLevel() < PersistentData.instance.m_ArmyGunUnlockLevel)
			{
				armyGun.guiText.text += "Locked (" + PersistentData.instance.m_ArmyGunUnlockLevel +")";
				armyGunBuy.guiText.material.color = Color.grey;
			}
			else
				armyGunBuy.guiText.material.color = Color.white;
			armyGun.guiText.material.color = Color.grey;
			armyGunBuy.guiText.text = "Buy (" + PersistentData.instance.m_ArmyGunPriceCoins + " Coins" +
										" & "+ PersistentData.instance.m_ArmyGunPriceGoldDiscs + " GD)";
		}
		// Have it already, update display
		else
		{
			armyGun.guiText.material.color = Color.white;
			armyGunBuy.guiText.material.color = Color.grey;
		}
		
		// Unless have the item
		if (!PersistentData.HasItem (PersistentData.Items.SPEEDWAYHELMET))
		{
			speedwayHelmet.guiText.text = "Speedway Helmet ";
			
			// If Locked, update display accordingly
			if (PersistentData.CurrentLevel() < PersistentData.instance.m_SpeedwayHelmetUnlockLevel)
			{
				speedwayHelmet.guiText.text += "Locked (" + PersistentData.instance.m_SpeedwayHelmetUnlockLevel +")";
				speedwayHelmetBuy.guiText.material.color = Color.grey;
			}
			else
				speedwayHelmetBuy.guiText.material.color = Color.white;
			speedwayHelmet.guiText.material.color = Color.grey;
			speedwayHelmetBuy.guiText.text = "Buy (" + PersistentData.instance.m_SpeedwayHelmetPriceCoins + " Coins" +
											" & "+ PersistentData.instance.m_SpeedwayHelmetPriceGoldDiscs + " GD)";
		}
		// Have it already, update display
		else
		{
			speedwayHelmet.guiText.material.color = Color.white;
			speedwayHelmetBuy.guiText.material.color = Color.grey;
		}
	}
		
	// Update is called once per frame
	void Update ()
	{
		UpdateGUITexts ();
		
		// 'Button' hittests and responses
		if (Input.GetMouseButtonUp (0))
		{
			// Back to previous screen
			if (back.guiText.HitTest (Input.mousePosition))
			{
				Application.LoadLevel ("startup");
			}
			// Debug only, reset save data
			else if (reset.guiText.HitTest (Input.mousePosition))
			{
				PersistentData.ResetAll ();
			}
			// Debug only, cheat 'win' XP
			else if (winXP.guiText.HitTest (Input.mousePosition))
			{
				PersistentData.xP += 100;
			}
			// Debug only, cheat 'win' Coins
			else if (winCoins.guiText.HitTest (Input.mousePosition))
			{
				PersistentData.coins += 10000;
			}
			// Buy GDs with Coins
			else if (goldDiscsBuyCoins.guiText.HitTest (Input.mousePosition))
			{
				// If have enough coins
				if (PersistentData.coins >= PersistentData.instance.m_GoldDiscsPriceCoins)
				{
					PersistentData.coins -= PersistentData.instance.m_GoldDiscsPriceCoins;
					PersistentData.goldDiscs ++;
				}
			}
			// But GDs with cash
			else if (goldDiscsBuyCash.guiText.HitTest (Input.mousePosition))
			{
				// IAP here
				PersistentData.cashSpentGBP += PersistentData.instance.m_GoldDiscsPriceCashGBP;
				PersistentData.goldDiscs ++;
			}
			// Upgrade
			else if (jumpsuitUpgrade.guiText.HitTest (Input.mousePosition))
			{
				// If at this upgrade level
				if (PersistentData.upgrades == PersistentData.Upgrades.JUMPSUIT_NONE)
				{
					// If unlocked and have the Coins and GDs, get it
					if (PersistentData.CurrentLevel() >= PersistentData.instance.m_JumpsuitRedUnlockLevel &&
						PersistentData.coins >= PersistentData.instance.m_JumpsuitRedPriceCoins &&
						PersistentData.goldDiscs >= PersistentData.instance.m_JumpsuitRedPriceGoldDiscs)
					{
						PersistentData.coins -= PersistentData.instance.m_JumpsuitRedPriceCoins;
						PersistentData.goldDiscs -= PersistentData.instance.m_JumpsuitRedPriceGoldDiscs;
						PersistentData.upgrades = PersistentData.Upgrades.JUMPSUIT_RED;
					}
				}
				// If at this upgrade level
				else if (PersistentData.upgrades == PersistentData.Upgrades.JUMPSUIT_RED)
				{
					// If unlocked and have the Coins and GDs, get it
					if (PersistentData.CurrentLevel() >= PersistentData.instance.m_JumpsuitSilverUnlockLevel &&
						PersistentData.coins >= PersistentData.instance.m_JumpsuitSilverPriceCoins &&
						PersistentData.goldDiscs >= PersistentData.instance.m_JumpsuitSilverPriceGoldDiscs)
					{
						PersistentData.coins -= PersistentData.instance.m_JumpsuitSilverPriceCoins;
						PersistentData.goldDiscs -= PersistentData.instance.m_JumpsuitSilverPriceGoldDiscs;
						PersistentData.upgrades = PersistentData.Upgrades.JUMPSUIT_SILVER;
					}
				}
				// If at this upgrade level
				else if (PersistentData.upgrades == PersistentData.Upgrades.JUMPSUIT_SILVER)
				{
					// If unlocked and have the Coins and GDs, get it
					if (PersistentData.CurrentLevel() >= PersistentData.instance.m_JumpsuitGoldUnlockLevel &&
						PersistentData.coins >= PersistentData.instance.m_JumpsuitGoldPriceCoins &&
						PersistentData.goldDiscs >= PersistentData.instance.m_JumpsuitGoldPriceGoldDiscs)
					{
						PersistentData.coins -= PersistentData.instance.m_JumpsuitGoldPriceCoins;
						PersistentData.goldDiscs -= PersistentData.instance.m_JumpsuitGoldPriceGoldDiscs;
						PersistentData.upgrades = PersistentData.Upgrades.JUMPSUIT_GOLD;
					}
				}
			}
			// If buying this item
			else if (blueSuedeShoesBuy.guiText.HitTest (Input.mousePosition))
			{
				// If unlocked and have the Coins and GDs, get it
				if (!PersistentData.HasItem (PersistentData.Items.BLUESUEDESHOES) &&
					PersistentData.CurrentLevel() >= PersistentData.instance.m_BlueSuedeShoesUnlockLevel &&
					PersistentData.coins >= PersistentData.instance.m_BlueSuedeShoesPriceCoins &&
					PersistentData.goldDiscs >= PersistentData.instance.m_BlueSuedeShoesPriceGoldDiscs)
				{
					PersistentData.coins -= PersistentData.instance.m_BlueSuedeShoesPriceCoins;
					PersistentData.goldDiscs -= PersistentData.instance.m_BlueSuedeShoesPriceGoldDiscs;
					PersistentData.items |= PersistentData.Items.BLUESUEDESHOES;
				}
			}
			// If buying this item
			else if (speedwayHelmetBuy.guiText.HitTest (Input.mousePosition))
			{
				// If unlocked and have the Coins and GDs, get it
				if (!PersistentData.HasItem (PersistentData.Items.SPEEDWAYHELMET) &&
					PersistentData.CurrentLevel() >= PersistentData.instance.m_SpeedwayHelmetUnlockLevel &&
					PersistentData.coins >= PersistentData.instance.m_SpeedwayHelmetPriceCoins &&
					PersistentData.goldDiscs >= PersistentData.instance.m_SpeedwayHelmetPriceGoldDiscs)
				{
					PersistentData.coins -= PersistentData.instance.m_SpeedwayHelmetPriceCoins;
					PersistentData.goldDiscs -= PersistentData.instance.m_SpeedwayHelmetPriceGoldDiscs;
					PersistentData.items |= PersistentData.Items.SPEEDWAYHELMET;
				}
			}
			// If buying this item
			else if (armyGunBuy.guiText.HitTest (Input.mousePosition))
			{
				// If unlocked and have the Coins and GDs, get it
				if (!PersistentData.HasItem (PersistentData.Items.ARMYGUN) &&
					PersistentData.CurrentLevel() >= PersistentData.instance.m_ArmyGunUnlockLevel &&
					PersistentData.coins >= PersistentData.instance.m_ArmyGunPriceCoins &&
					PersistentData.goldDiscs >= PersistentData.instance.m_ArmyGunPriceGoldDiscs)
				{
					PersistentData.coins -= PersistentData.instance.m_ArmyGunPriceCoins;
					PersistentData.goldDiscs -= PersistentData.instance.m_ArmyGunPriceGoldDiscs;
					PersistentData.items |= PersistentData.Items.ARMYGUN;
				}
			}
		}
	}
	
}
