using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ICreature : MonoBehaviour
{
	public enum CreatureState 
	{
		NORMAL,
		SLOWED
	}

	public CreatureState currentState;

	public bool AddedToBestiary = true;

	public string Name { get; set; }

	public string Description { get; set; }

	public int Level { get; set; }

	public int RewardExperiencePoints {get; set;}

	public float MaximumHitPoints { get; set; }

	public float CurrentHitPoints { get; set; }

	public float CreatureDamage { get; set;}

	public int EnergyReward { get; set; }

	public float FearEmissionPerSecond { get; set;}

	private bool died;

	public Sprite CreatureImage;

	public GameObject DeathEffect;

	public GameObject HealthBar;


	float originalSpeed;


	// Default Constructor
	public ICreature(string name, string desc, int level, int rewardExperience, 
		int maximumHitPoints, int currentHitPoints, int energyReward, float fearemmision, float damage)
	{
		Name = name;
		Description = desc;
		Level = level;
		RewardExperiencePoints = rewardExperience;
		MaximumHitPoints = maximumHitPoints;
		CurrentHitPoints = currentHitPoints;
		EnergyReward = energyReward;
		FearEmissionPerSecond = fearemmision;
		CreatureDamage = damage;

	}

	void Awake()
	{
		originalSpeed = gameObject.GetComponent <MonsterMovement> ().speed;
	}

	void Update()
	{
		switch (currentState) 
		{
		case CreatureState.NORMAL:
			gameObject.GetComponent <MonsterMovement> ().speed = originalSpeed;
			break;
		case CreatureState.SLOWED:
			Debug.Log ("I AM SLOWED :(");
			break;
		default:
			break;
		}

		CreatureDied ();
	}

	public void CreatureDied()
	{
		if(CurrentHitPoints <= 0)
		{
			// Destroy gameObject and initiate Particle Death effect
			if (died == false) 
			{
				Instantiate (DeathEffect,new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), DeathEffect.transform.rotation);
				SoundManager._instance.CreatureDied.Play ();
				GameManager._instance.player.creaturesKilled += 1;
				died = true;
				Destroy(gameObject);


			}

			// Give XP to the player
			GameManager._instance.player.CurrentExperiencePoints += this.RewardExperiencePoints;

			// Give energy to the player

			if(GameManager._instance.player.CurrentEnergy + this.EnergyReward < GameManager._instance.player.MaximumEnergy)

			{
				GameManager._instance.player.CurrentEnergy += this.EnergyReward;	
			}
			else
			{
				GameManager._instance.player.CurrentEnergy = GameManager._instance.player.MaximumEnergy;
			}

		}
			
	}




				
}


