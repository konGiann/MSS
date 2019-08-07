using System.Collections.Generic;

public class PlayerStats 
{
	public  int creaturesKilled = 0;

	public string Name { get; set; }

	public int Level { get; set; }

	public int CurrentExperiencePoints;

	public int ExperiencePointsNeeded;

	public int MaximumFear { get; set; } // This is HEALTH

	public float CurrentFear { get; set; }

	public int CurrentEnergy { get; set; }

	public int MaximumEnergy {get; set; }

	public int PointsToSpend { get; set; } // Points that player can spend on level up to increase his attributes

	public List<DefensiveItems> ItemsBook { get; set; }

	public List<ICreature> CreatureBestiary { get; set;}

	public PlayerStats (string name, int level, int maxFear, int maxEnergy, int curXP, int xpNeeded, int curEnergy)
	{
		Name = name;
		Level = level;
		MaximumFear = maxFear;
		MaximumEnergy = maxEnergy;
		CurrentExperiencePoints = curXP;
		ExperiencePointsNeeded = xpNeeded;
		CurrentEnergy = curEnergy;

	}

	// Function to add every new creature the player encounters in the level, to his bestiary
	public void AddCreatureToBestiary(ICreature creature)
		{
			if (GameManager._instance.player.CreatureBestiary == null) 
			{
				GameManager._instance.player.CreatureBestiary = new List<ICreature> ();
				GameManager._instance.player.CreatureBestiary.Add (creature);
				return;
			}
						
				GameManager._instance.player.CreatureBestiary.Add (creature);
		}

	public void PlayerLevelUp()
	{
		if (CurrentExperiencePoints >= ExperiencePointsNeeded)
		{
            Level += 1;
            switch (Level)
            {
                case 2:
                case 3:
                case 4:
                    PointsToSpend += 2;
                    break;
                case 5:
                case 6:
                case 7:
                    PointsToSpend += 3;
                    break;
                case 8:
                case 9:
                case 10:
                    PointsToSpend += 4;
                    break;
                default:
                    break;
            }            
			
            //Next level up
            switch (Level)
            {
                case 2:
                case 3:
                case 4:
                    ExperiencePointsNeeded = (int)(ExperiencePointsNeeded * 1.5);
                    break;
                case 5:
                case 6:
                case 7:
                    ExperiencePointsNeeded = (int)(ExperiencePointsNeeded * 1.8);
                    break;
                case 8:
                case 9:
                case 10:
                    ExperiencePointsNeeded = (int)(ExperiencePointsNeeded * 2);
                    break;
                default:
                    break;
            }          
		}
	}
}
