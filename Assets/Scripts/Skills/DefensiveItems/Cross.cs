public class Cross : DefensiveItems {

	public Cross()
	{
		Name = "Cross of Faith";
		LevelRequired = 2;
		EnergyRequiredToCraft = 1;
		MaximumHitPoints = 15;
		CurrentHitPoints = 15;
		ItemDamagePerSec = 1.8f;
        AttackUpgradeCost = 3;
        HealthUpgradeCost = 2;
        Description = "It deals damage in a circular radius and can hit emenies in adjacent lanes.";
	}


}
