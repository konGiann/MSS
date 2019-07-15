using System.Collections.Generic;
using UnityEngine;

public class SkillBar : MonoBehaviour {

    public Texture2D image;
    
	public Rect position;

	public const int NUMBER_OF_ITEMS = 3; // Change this number when more items ll be added in the future

	public List<DefensiveItems> unlockedDefensiveItems;

	public SkillSlot[] skillSlotsArray = new SkillSlot[NUMBER_OF_ITEMS];
	public DefensiveItems[] convertKeysToArray = new DefensiveItems[NUMBER_OF_ITEMS];

	public static SkillBar _instance = null;


    public float skillX;
    public float skillY;
    public float skillWidth;
    public float skillHeight;
    public float skillDistance;

	void Awake()
	{
		if (_instance == null)
			_instance = this;
		else if (_instance != null)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);

		unlockedDefensiveItems = new List<DefensiveItems> ();

	}

	void Update()
	{
		AssignSkillSlots ();

		// Set proper distance for every item slot in Skillbar


		// Press numbers 1-9 to select a skill from the Skillbar and get it active on Cursor.Position. 
		UseSkill ();

		// Spawn Defensive Item if mouse is over a valid floor square
		//SpawnItem ();


	}

    void OnGUI()
    {
		// Draw the skillbar image
        DrawSkillBar();

		// Draw the image of every available to the player skill, on the Skillbar


    }

	#region Methods


	void AssignSkillSlots ()
	{
		convertKeysToArray = unlockedDefensiveItems.ToArray();

		for (int i = 0; i < NUMBER_OF_ITEMS; i++) 
		{
			skillSlotsArray [i] = new SkillSlot ();
			skillSlotsArray [i].defensiveItem = convertKeysToArray[i];
		}

		skillSlotsArray [0].key = KeyCode.Alpha1;
		skillSlotsArray [1].key = KeyCode.Alpha2;
		skillSlotsArray [2].key = KeyCode.Alpha3;
	}

	void UseSkill()
	{
		foreach (var slot in skillSlotsArray) 
		{

			// If we press the key of the slot, draw the image of the skill at mouse position. 
			// The actual Draw is in a function inside Defensige items. We allow it to be called from here
			if(Input.GetKeyUp (slot.key))
			{
				
				slot.defensiveItem.drawOnMouse = !slot.defensiveItem.drawOnMouse;

			}

			// If skill is drawn and we press any K/B key, remove the drawn Texture and if the key pressed was a slot, draw the new one
			if (slot.defensiveItem.drawOnMouse == true && Input.anyKeyDown)
			{
				if (Input.GetMouseButton (0)
				   || Input.GetMouseButton (1)
				   || Input.GetMouseButton (2)
				    || Input.GetKeyDown (KeyCode.A)		
					|| Input.GetKeyDown (KeyCode.D)
					|| Input.GetKeyDown (KeyCode.S)
					|| Input.GetKeyDown (KeyCode.W)
				)
					return;
				else
				{
					slot.defensiveItem.drawOnMouse = false;

				}
			}
		}

	}

	//public void SpawnItem()
	//{
		
	//	foreach (var slot in skillSlotsArray) 
	//	{
	//		// If skill is drawn at mouse position, left click to spawn it at mouse position
	//		if(slot.defensiveItem.drawOnMouse == true && Input.GetMouseButtonDown (0) && slot.defensiveItem.NumberAvailable > 0)
	//		{
	//			Vector2 mousePos;
	//			Vector2 targetPosition;

	//			mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
	//			targetPosition = new Vector2 (Mathf.Round (mousePos.x), Mathf.Round (mousePos.y));

	//			//Instantiate (slot.defensiveItem.Pref, targetPosition, Quaternion.identity);	

	//			slot.defensiveItem.NumberAvailable -= 1;
	//		}
	//	}
	//}



	void DrawSelected()
	{
		GUI.Label (new Rect (0, 0, 100, 100), "Selected");
	}

	void DrawSkillBar()
    {
        GUI.DrawTexture(GetScreenRect(position), image);
    }

  
    Rect GetScreenRect(Rect position)
    {
        return new Rect(Screen.width * position.x, Screen.height * position.y, Screen.width * position.width, Screen.height * position.height);
    }

	#endregion
}
