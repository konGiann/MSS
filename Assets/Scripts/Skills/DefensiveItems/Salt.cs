public class Salt : DefensiveItems
{
	// Special for Salt

	public float SlowEffect { get; set; }

	public Salt()
	{
		Name = "Salt";
		LevelRequired = 1;
		EnergyRequiredToCraft = 1;
		MaximumHitPoints = 15;
		CurrentHitPoints = 15;
		HealthUpgradeCost = 2;
		ItemDamagePerSec = 2.3f;
		AttackUpgradeCost = 2;
		Description = "just simple salt. Moslty averts lesser deamons from passing, or slows down higher daemons";
		SlowEffect = 0.6f;
	}


}
