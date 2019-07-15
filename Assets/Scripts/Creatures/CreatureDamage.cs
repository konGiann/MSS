using UnityEngine;
using System.Collections;

public class CreatureDamage : MonoBehaviour 
{
	ICreature creature = null;
	DefensiveItems defItem = null;

	// Aply Damage
	void OnTriggerStay2D(Collider2D item)
	{
		creature = gameObject.GetComponentInParent <ICreature> ();
		defItem = item.gameObject.GetComponentInParent <DefensiveItems> ();

		if(item.gameObject.tag == "Health")
		{
			float calc_health = defItem.CurrentHitPoints / defItem.MaximumHitPoints;
			
			defItem.CurrentHitPoints -= creature.CreatureDamage * Time.smoothDeltaTime;

			defItem.Healthbar.transform.localScale = new Vector3 (calc_health, 1, 1);
		}
	}

}
