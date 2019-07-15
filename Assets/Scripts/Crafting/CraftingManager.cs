using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour {

	public Texture2D image;

	public Rect position;

	public DefensiveItems[] AvailableForCrafting = new DefensiveItems[SkillBar.NUMBER_OF_ITEMS];

	public bool drawCraftingPanel;

	public float skillX;
	public float skillY;
	public float skillWidth;
	public float skillHeight;
	public float skillDistance;


	void Update()
	{
		AssignDefensiveItems ();

		// Press "C" on keyboard to show hide the crafting panel
		//ShowHideCraftingPanel ();
	}

	void OnGUI () 
	{
//		DrawCraftingPanel ();
//
//		SetSlotsDistance ();

		DrawCraftableItems ();	
	}

	// Assign all unlocked items to the crafting bar
	void AssignDefensiveItems ()
	{
		for (int i = 0; i < SkillBar._instance.convertKeysToArray.Length; i++) {
			AvailableForCrafting [i] = SkillBar._instance.convertKeysToArray [i];
		}
	}

	// Draw Sprites of items as buttons. Display the amount of energy required to craft for each one.
	// Implement the crafting logic for player to be able to craft items.
	void DrawCraftableItems ()
	{
//		if(drawCraftingPanel)
//		{
//			foreach (var item in AvailableForCrafting) 
//			{
//				GUI.Label (GetScreenRect (item.position), item.EnergyRequiredToCraft.ToString ());
//				if(GUI.Button (GetScreenRect (item.position), item.Image))
//				{
//
//					if(GameManager._instance.player.CurrentEnergy >= item.EnergyRequiredToCraft)
//					{
//						item.NumberAvailable += 1;
//						GameManager._instance.player.CurrentEnergy -= item.EnergyRequiredToCraft;
//					}
//					else if(GameManager._instance.player.CurrentEnergy < item.EnergyRequiredToCraft)
//					{
//						GUIManager._instance.GameMessages.text = "Not enough ENERGY to craft!";
//						StartCoroutine (GUIManager._instance.DeleteGameMessageTextAfterSeconds (2));
//					}
//				}

//			}
//		}

	}

	void DrawCraftingPanel ()
	{
		if(drawCraftingPanel)
		{
			GUI.DrawTexture (GetScreenRect (position), image);
		}

	}

	// Make them pretty
//	void SetSlotsDistance ()
//	{
//		for (int i = 0; i < AvailableForCrafting.Length; i++) {
//			AvailableForCrafting [i].position.Set (skillX + i * (skillWidth + skillDistance), skillY, skillWidth, skillHeight);
//		}
//	}

	void ShowHideCraftingPanel ()
	{
		if (Input.GetKeyDown (KeyCode.C))
			drawCraftingPanel = !drawCraftingPanel;
	}

	// Give a position to this function and will place the texture related to Screen pixels
	Rect GetScreenRect(Rect position)
	{
		return new Rect(Screen.width * position.x, Screen.height * position.y, Screen.width * position.width, Screen.height * position.height);
	}

}
