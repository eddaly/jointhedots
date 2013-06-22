using UnityEngine;
using System.Collections;

public class FE_PersistentData : MonoBehaviour {

	GameObject back, reset, cashSpentGBP, highScore, xp, winXP, coins, winCoins, goldDiscs, goldDiscsBuyCoins, goldDiscsBuyCash, jumpsuit, jumpsuitUpgrade;
	GameObject blueSuedeShoes, blueSuedeShoesBuy, armyGun, armyGunBuy, speedwayHelmet, speedwayHelmetBuy;

	// Use this for initialization
	void Start () {
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
	
	void UpdateGUITexts ()
	{
		cashSpentGBP.guiText.text = "Cash Spent (GBP) " + PersistentData.cashSpentGBP;
		
		highScore.guiText.text = "High Score " + PersistentData.hiScore;
		xp.guiText.text = "XP " + PersistentData.xP + " Level: " + PersistentData.CurrentLevel();
		coins.guiText.text = "Coins " + PersistentData.coins;
		goldDiscs.guiText.text = "Gold Discs " + PersistentData.goldDiscs;
		goldDiscsBuyCoins.guiText.text = "Buy with Coins (" + PersistentData.instance.m_GoldDiscsPriceCoins + ")";
		goldDiscsBuyCash.guiText.text = "Buy with Cash (" + PersistentData.instance.m_GoldDiscsPriceCashGBP + "GBP)";
		
		if (PersistentData.jumpsuitUpgrades == PersistentData.JumpsuitUpgrades.NONE)
		{
			jumpsuit.guiText.text = "Jumpsuit NONE";
	
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
		else if (PersistentData.jumpsuitUpgrades == PersistentData.JumpsuitUpgrades.RED)
		{
			jumpsuit.guiText.text = "Jumpsuit RED";
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
		else if (PersistentData.jumpsuitUpgrades == PersistentData.JumpsuitUpgrades.SILVER)
		{
			jumpsuit.guiText.text = "Jumpsuit SILVER";
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
		else// if (PersistentData.jumpsuitUpgrades == PersistentData.JumpsuitUpgrades.GOLD)
		{
			jumpsuit.guiText.text = "Jumpsuit GOLD";
			jumpsuitUpgrade.guiText.material.color = Color.grey;
		}
		
		if (!PersistentData.HasItem (PersistentData.Items.BLUESUEDESHOES))
		{
			blueSuedeShoes.guiText.text = "Blue Suede Shoes ";
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
		else
		{
			blueSuedeShoes.guiText.material.color = Color.white;			
			blueSuedeShoesBuy.guiText.material.color = Color.grey;
		}
		if (!PersistentData.HasItem (PersistentData.Items.ARMYGUN))
		{
			armyGun.guiText.text = "Army Gun ";
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
		else
		{
			armyGun.guiText.material.color = Color.white;
			armyGunBuy.guiText.material.color = Color.grey;
		}
		if (!PersistentData.HasItem (PersistentData.Items.SPEEDWAYHELMET))
		{
			speedwayHelmet.guiText.text = "Speedway Helmet ";
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
			if (back.guiText.HitTest (Input.mousePosition))
			{
				Application.LoadLevel ("startup");
			}
			else if (reset.guiText.HitTest (Input.mousePosition))
			{
				PersistentData.ResetAll ();
			}
			else if (winXP.guiText.HitTest (Input.mousePosition))
			{
				PersistentData.xP += 100;
			}
			else if (winCoins.guiText.HitTest (Input.mousePosition))
			{
				PersistentData.coins += 10000;
			}
			else if (goldDiscsBuyCoins.guiText.HitTest (Input.mousePosition))
			{
				if (PersistentData.coins >= PersistentData.instance.m_GoldDiscsPriceCoins)
				{
					PersistentData.coins -= PersistentData.instance.m_GoldDiscsPriceCoins;
					PersistentData.goldDiscs ++;
				}
			}
			else if (goldDiscsBuyCash.guiText.HitTest (Input.mousePosition))
			{
				// IAP here
				PersistentData.cashSpentGBP += PersistentData.instance.m_GoldDiscsPriceCashGBP;
				PersistentData.goldDiscs ++;
			}
			else if (jumpsuitUpgrade.guiText.HitTest (Input.mousePosition))
			{
				if (PersistentData.jumpsuitUpgrades == PersistentData.JumpsuitUpgrades.NONE)
				{
					if (PersistentData.CurrentLevel() >= PersistentData.instance.m_JumpsuitRedUnlockLevel &&
						PersistentData.coins >= PersistentData.instance.m_JumpsuitRedPriceCoins &&
						PersistentData.goldDiscs >= PersistentData.instance.m_JumpsuitRedPriceGoldDiscs)
					{
						PersistentData.coins -= PersistentData.instance.m_JumpsuitRedPriceCoins;
						PersistentData.goldDiscs -= PersistentData.instance.m_JumpsuitRedPriceGoldDiscs;
						PersistentData.jumpsuitUpgrades = PersistentData.JumpsuitUpgrades.RED;
					}
				}
				else if (PersistentData.jumpsuitUpgrades == PersistentData.JumpsuitUpgrades.RED)
				{
					if (PersistentData.CurrentLevel() >= PersistentData.instance.m_JumpsuitSilverUnlockLevel &&
						PersistentData.coins >= PersistentData.instance.m_JumpsuitSilverPriceCoins &&
						PersistentData.goldDiscs >= PersistentData.instance.m_JumpsuitSilverPriceGoldDiscs)
					{
						PersistentData.coins -= PersistentData.instance.m_JumpsuitSilverPriceCoins;
						PersistentData.goldDiscs -= PersistentData.instance.m_JumpsuitSilverPriceGoldDiscs;
						PersistentData.jumpsuitUpgrades = PersistentData.JumpsuitUpgrades.SILVER;
					}
				}
				else if (PersistentData.jumpsuitUpgrades == PersistentData.JumpsuitUpgrades.SILVER)
				{
					if (PersistentData.CurrentLevel() >= PersistentData.instance.m_JumpsuitGoldUnlockLevel &&
						PersistentData.coins >= PersistentData.instance.m_JumpsuitGoldPriceCoins &&
						PersistentData.goldDiscs >= PersistentData.instance.m_JumpsuitGoldPriceGoldDiscs)
					{
						PersistentData.coins -= PersistentData.instance.m_JumpsuitGoldPriceCoins;
						PersistentData.goldDiscs -= PersistentData.instance.m_JumpsuitGoldPriceGoldDiscs;
						PersistentData.jumpsuitUpgrades = PersistentData.JumpsuitUpgrades.GOLD;
					}
				}
			}
			else if (blueSuedeShoesBuy.guiText.HitTest (Input.mousePosition))
			{
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
			else if (speedwayHelmetBuy.guiText.HitTest (Input.mousePosition))
			{
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
			else if (armyGunBuy.guiText.HitTest (Input.mousePosition))
			{
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
