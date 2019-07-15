using UnityEngine;
using System.Collections;

public class SaltSlowEffect : MonoBehaviour 
{
	MonsterMovement creatureMovement;
	ICreature creatureStats;
	Salt slowEffect;

	bool slowed;

	public float originalCreatureSpeed; // to reassign it on trigger exit. That happend on creatures script method

	// Slows enemies' movement speed AND attack speed

	void OnTriggerEnter2D(Collider2D creature)
	{
		slowed = true;
	}

	void OnTriggerStay2D(Collider2D creature)
	{
		creatureMovement = creature.gameObject.GetComponent<MonsterMovement> ();

		slowEffect = gameObject.GetComponentInParent <Salt> ();

		creatureStats = creature.gameObject.GetComponent <ICreature> ();


		if(creature.tag == "Enemy" && slowed == true)
		{	
		
			creatureMovement.speed -= creatureMovement.speed * slowEffect.SlowEffect;
			creatureStats.currentState = ICreature.CreatureState.SLOWED;

			slowed = false;



		}
	}

	void OnTriggerExit2D(Collider2D creature)
	{
		if(creature.tag == "Enemy")
		{
			creatureStats = creature.gameObject.GetComponent <ICreature> ();
			creatureStats.currentState = ICreature.CreatureState.NORMAL;
			Debug.Log ("EXIT!");
		}
			
	}
}
