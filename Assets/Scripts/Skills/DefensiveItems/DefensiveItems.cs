using UnityEngine;
using System.Collections;

public class DefensiveItems : MonoBehaviour
{
	private int _numberAvailable = 10;
	
	public string Name { get; set; }

	public int NumberAvailable { get {return _numberAvailable; } set { _numberAvailable = value; } }

	public int LevelRequired  { get; set;}

	public int EnergyRequiredToCraft  { get; set;}

	public string Description { get; set; }

	#region Health
    
	public float MaximumHitPoints { get; set; }

	public float CurrentHitPoints { get; set; }

	public int HealthUpgradeCost { get; set; }
	#endregion

	#region Damage
	public float ItemDamagePerSec { get; set; } public int AttackUpgradeCost { get; set; }

	public float InitialBonusDamage {get;set;}
	#endregion

	public Texture2D Image;
//	public GameObject Pref;
	public GameObject DestroyedEffect;
	public GameObject Healthbar;
	public GameObject Floor;

	public bool IsUnlocked;
	public bool destroyed = false;
	public bool drawOnMouse;
    
    #region Apply damage to creatures
    // Creature reference
    ICreature creatureScript = null;

	// Damage while inside radius (applied every second)
	void OnTriggerStay2D(Collider2D creature)
	{
		// Get script component from enemy so we can interact with its STATS
		creatureScript = creature.gameObject.GetComponent <ICreature> ();

		// Apply damage
		if (creature.gameObject.tag == "Enemy")

		{
			float calc_health = creatureScript.CurrentHitPoints / creatureScript.MaximumHitPoints;
			creatureScript.CurrentHitPoints -= ItemDamagePerSec * Time.smoothDeltaTime;


			creatureScript.HealthBar.transform.localScale = new Vector3 (calc_health, 1, 1);
		}

		if(destroyed == true)
		{		
            Destroy(this);
		}
	}

	// Bonus item damage when creatures entes radius aka IBD  (applied ONCE)
	void OnTriggerEnter2D(Collider2D creature)
	{
		// Get script component from enemy so we can interact with its STATS
		creatureScript = creature.gameObject.GetComponent <ICreature> ();

		// Apply damage
		if (creature.gameObject.tag == "Enemy")
		{
			creatureScript.CurrentHitPoints -= InitialBonusDamage;
		}
	}
	#endregion

	void ItemDestroyed()
	{
		if(CurrentHitPoints < 1)
		{
			if(destroyed == false)
			{
				destroyed = true;
				Instantiate (DestroyedEffect, new Vector3(gameObject.transform.position.x,
				gameObject.transform.position.y, gameObject.transform.position.z), DestroyedEffect.transform.rotation);
				Floor.tag = "Floor";
				transform.position = Vector3.up * 100;
				StartCoroutine ("CountDownForDestruction");
			}
		}
	}

	// Draw item at cursor position if we prees a slotskill hotkey
	void DrawItmeAtCursor()
	{
		if (drawOnMouse == true)
		{
			GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 50, 50), Image);
            
		}
	}

	void OnGUI()
	{
		DrawItmeAtCursor ();
	}

	void FixedUpdate()
	{
		ItemDestroyed ();
	}

	IEnumerator CountDownForDestruction()
	{
		yield return new WaitForSeconds (1);		
        Destroy(gameObject);
	}
}

