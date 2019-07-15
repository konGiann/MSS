using UnityEngine;
using System.Collections;

public class HolyWater : DefensiveItems {

	public HolyWater()
	{
		Name = "Holy Water";
		LevelRequired = 1;
		EnergyRequiredToCraft = 1;
		MaximumHitPoints= 15;
		CurrentHitPoints = 15;
		ItemDamagePerSec = 5;
	}

}
