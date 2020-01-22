using UnityEngine;
using System.Collections;

public class SaltSlowEffect : MonoBehaviour
{
    MonsterMovement creatureMovement;
    ICreature creatureStats;
    Salt slowEffect;



    public float originalCreatureSpeed; // to reassign it on trigger exit. That happend on creatures script method

    // Slows enemies' movement speed AND attack speed

    void OnTriggerEnter2D(Collider2D creature)
    {

        creatureMovement = creature.gameObject.GetComponent<MonsterMovement>();

        slowEffect = gameObject.GetComponentInParent<Salt>();

        creatureStats = creature.gameObject.GetComponent<ICreature>();


        if (creature.tag == "Enemy")
        {            
            creatureMovement.speed -= creatureMovement.speed * slowEffect.SlowEffect;
            creatureStats.currentState = ICreature.CreatureState.SLOWED;

            Debug.Log("SLOWED SPEED: " + creatureMovement.speed);
        }
    }

    void OnTriggerExit2D(Collider2D creature)
    {
        if (creature.tag == "Enemy")
        {
            creatureMovement = creature.gameObject.GetComponent<MonsterMovement>();
            creatureStats = creature.gameObject.GetComponent<ICreature>();
            creatureStats.currentState = ICreature.CreatureState.NORMAL;
            Debug.Log("NORMAL SPEED: " + creatureMovement.speed);
            Debug.Log("EXIT!");
        }

    }
}
