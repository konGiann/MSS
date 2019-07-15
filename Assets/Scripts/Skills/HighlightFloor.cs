using UnityEngine;
using System.Collections;

public class HighlightFloor : MonoBehaviour 
{

	// Sprite of the floor object
	SpriteRenderer sr;

	// Reference to the player GO. We will need to know his distance from each floor GO
	GameObject player;

	DefensiveItems itemScript;

	// We will store the calculated distance to this field
	float distance;

	// Set the desired distance in the inspector
	public float ValidFloorDistance;

	void Awake()
	{
		sr = GetComponent <SpriteRenderer> ();

		player = GameObject.FindGameObjectWithTag ("Player");

	
	}

	void Update()
	{
		distance = Vector3.Distance (this.transform.position, player.transform.position);
     
	}

	// When hovering over the floor, check if the distance of the player to the floor tile is smaller or equal
	// To the desired distance we already set. If it is, the we set the floor's tag to VALID and we can 
	// run the method to instantiate the prefab we have chose throught numeric keys.
	void OnMouseOver()
	{
		if(Mathf.Round (distance) <= ValidFloorDistance && this.tag != "FloorOccupied")
		{
			sr.color = Color.green; // Change color to green for EFFECT
			tag = "FloorValid"; // Change Tag

			NewSkillBar._instance.PlaceItem (gameObject);
		}		
	}

	void OnMouseExit()
	{
		sr.color = Color.white;
		if(this.tag != "FloorOccupied")
			this.tag = "Floor";

        Debug.Log("TAG: " + tag + "DISTANCE: " + distance);        
    }

	void OnTriggerStay2D(Collider2D item)
	{
		
		itemScript = item.gameObject.GetComponentInParent <DefensiveItems> ();
		if(item.gameObject.tag == "FloorCollider")
		{			
			itemScript.Floor = this.gameObject;
		}
	}



}
