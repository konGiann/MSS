using UnityEngine;
using System.Collections;
using System;

public class FearEmissionPerSecond : MonoBehaviour {


	private ICreature creatureScript = null;    

    void OnTriggerEnter2D(Collider2D creature)
	{
		// Get script component from enemy so we can interact with its STATS
		creatureScript = creature.gameObject.GetComponentInParent <ICreature> ();

		// Aply 1 point of fear instantly on ENTER
		if (creature.gameObject.tag == "Fear")
        {
            GameManager._instance.player.CurrentFear += 1;
            // Change fear.text font to red on ENTER
            GUIManager._instance.Fear.color = Color.red;
        }
			

		
	}

	void OnTriggerStay2D(Collider2D creature) 
	{
		// Get script component from enemy so we can interact with its STATS
		creatureScript = creature.gameObject.GetComponentInParent <ICreature> ();

		

		if (creature.gameObject.tag == "Fear") 
		{
            // Change fear.text font to red on STAY
            GUIManager._instance.Fear.color = Color.red;

            // Increase the Fear level of player every second, 
            // multiplying it with the Fear emission level of the enemy creature
            GameManager._instance.player.CurrentFear += creatureScript.FearEmissionPerSecond
			* Time.smoothDeltaTime;

            // Warn the player - on STAY - with a message if his current fear level exceeds the 70% of his total fear points            
            if (GameManager._instance.player.CurrentFear > GameManager._instance.player.MaximumFear * 0.8)
            {       
                if(!GUIManager._instance.CoroutineIsRunning)
                {                    
                    StartCoroutine(GUIManager._instance.ShowMessage("You fear level has dramatically increased. Stay away from the monsters!", 2));                    
                }                
            }
        }					
	}

	void OnTriggerExit2D(Collider2D creature)
	{
		// Back to normal fear.text color
		GUIManager._instance.Fear.color = Color.white;

		// Start coroutine to dissapear the warning text		

	}
	
}
