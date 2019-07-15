using UnityEngine;
using System.Collections;

public class EmptySkill : DefensiveItems 
{
	public EmptySkill()
	{
		Name = "Empty";
		LevelRequired = 0;
		EnergyRequiredToCraft = 0;
		IsUnlocked = true;
		CurrentHitPoints = 1;
		NumberAvailable = 0;
	}

}
