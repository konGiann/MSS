using System;
using UnityEngine;
using UnityEngine.UI;

public class NewSkillBar : MonoBehaviour {

	public static NewSkillBar _instance; // For singleton purpose

	public GameObject[] slots;

	public SkillSlot[] skillSlotsArray;

	public Text[] numberAvailableArray;

	void Awake()
	{
		if (_instance == null)
			_instance = this;
		else if (_instance != null)
			Destroy (gameObject);
		
		skillSlotsArray = new SkillSlot[5];

	}
    
	void Update()
	{
		AssignSlotsAndSkills ();

		UseSkill ();
        
    }

    void AssignSlotsAndSkills ()
	{
		for (int i = 0; i < slots.Length; i++) 
		{
			// Create an new SKillSlot class for every Slot gameobject
			skillSlotsArray [i] = new SkillSlot ();

			// Set the defensive item of SkillSlot class to the Defensive item of the first child gameobject of the Slot gameobject
			// There is always a gameobject with an Empty Skill class. So, when it is the only child, it sets the slot defensive item to "Empty SKill"
			skillSlotsArray [i].defensiveItem = slots[i].gameObject.transform.GetChild (0).GetComponent <DefensiveItems>();

			// Assing Text value of number of uses available
			numberAvailableArray[i].text = skillSlotsArray[i].defensiveItem.NumberAvailable.ToString ();
		}

		// Assign the keyboard keys for every Slot (numerics 1-5)
		skillSlotsArray [0].key = KeyCode.Alpha1;
		skillSlotsArray [1].key = KeyCode.Alpha2;
		skillSlotsArray [2].key = KeyCode.Alpha3;
		skillSlotsArray [3].key = KeyCode.Alpha4;
		skillSlotsArray [4].key = KeyCode.Alpha5;
	}

	void UseSkill()
	{
		foreach (var slot in skillSlotsArray) 
		{

			// If we press the key of the slot, draw the image of the skill at mouse position. 
			// The actual Draw is in a function inside Defensive items. We allow it to be called from here
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

	public void PlaceItem(GameObject gameObject)
	{

        if (gameObject != null)
        {
            try
            {
                foreach (var slot in skillSlotsArray)
                {
                    // If skill is drawn at mouse position, left click to spawn it at mouse position on a valid floor.
                    if (slot.defensiveItem.drawOnMouse == true && Input.GetMouseButtonDown(0) && slot.defensiveItem.NumberAvailable > 0)
                    {
                        Vector2 mousePos;
                        Vector3 targetPosition;

                        // Get mouse position translated to wold cords
                        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                        // Set the target position with round so it will be placed on floor tile
                        targetPosition = new Vector3(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y), -4);

                        // Instantiate prefab
                        DefensiveItems instaPref = (DefensiveItems)Instantiate(slot.defensiveItem, targetPosition, Quaternion.identity);

                        // Play sound
                        SoundManager._instance.ItemPlaced.Play();

                        // Copy values to instance
                        instaPref.ItemDamagePerSec = slot.defensiveItem.ItemDamagePerSec;
                        instaPref.InitialBonusDamage = slot.defensiveItem.InitialBonusDamage;

                        instaPref.MaximumHitPoints = slot.defensiveItem.MaximumHitPoints;
                        instaPref.CurrentHitPoints = instaPref.MaximumHitPoints;



                        // Get Rect transform so we can reset its width and height - defined by grid layout. Otherwise healthbar won't be visible
                        RectTransform rt = instaPref.GetComponent<RectTransform>();
                        rt.sizeDelta = new Vector2(0, 0);

                        // Instantly, set the INSTANTIATED object's drawOnMouse to false, else we will forever have its image drawn at cursor pos
                        instaPref.drawOnMouse = false;

                        // Decrease the amount avaiable of the Defensive Item that was used
                        slot.defensiveItem.NumberAvailable -= 1;

                        // Change the floor pref tag so we cannot place any other object on top
                        gameObject.tag = "FloorOccupied";
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);                
            }
        }
	}
}
