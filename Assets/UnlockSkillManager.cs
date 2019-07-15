using UnityEngine;
using UnityEngine.UI;

public class UnlockSkillManager : MonoBehaviour {

	public GameObject[] AllSkills;
	public Transform Slot;

	public Text SkillName;
	public Text ItemAttackValue;
	public Text ItemHealthValue;
	public Text ItemDescription;

    DefensiveItems defInstance;

	private Animator anim; 

	void Awake()
	{
		// Get panel's animator
		anim = gameObject.GetComponent<Animator>();

		ResetItemUnlockedState ();
	}

	void Update()
	{
		/*We set in the editor all the skill prefabs. If a skill get unlocked (matching its level with player's level), 
		the panel is shown with the skill's prefab in its slot */
		foreach (var item in AllSkills) 
		{
			var skill = item.GetComponent <DefensiveItems>();
			
			if(GameManager._instance.player.Level == skill.LevelRequired && skill.IsUnlocked == false)
			{
				
				OpenPanel();
				var instance = Instantiate (item, new Vector3(0,0,0), Quaternion.identity) as GameObject;
				instance.transform.SetParent(Slot, false);
				skill.IsUnlocked = true; // set it to true to avoid infinite instantiation
				SkillName.text = skill.Name;

				// Play sound
				SoundManager._instance.ItemUnlocked.Play ();

                defInstance = instance.GetComponent<DefensiveItems>();

			}
		}

        if(Slot.childCount != 0)
        {
            ItemAttackValue.text = defInstance.ItemDamagePerSec.ToString();
            ItemHealthValue.text = defInstance.MaximumHitPoints.ToString();
            ItemDescription.text = defInstance.Description;
        }

        ClosePanel();
        

    }

	public void OpenPanel()
	{
		anim.enabled = true;
		anim.Play("NewSkillUnlockedPanelIn");
	}

	public void ClosePanel()
	{
		if(Slot.childCount == 0 && Input.GetMouseButtonUp(0)) // Close button will only work if the skill is dragged away from the slot
			anim.Play("NewSkillUnlockedPanelOut");
	}

	void ResetItemUnlockedState ()
	{
		foreach (var item in AllSkills) {
			var skill = item.GetComponent<DefensiveItems> ();
			skill.IsUnlocked = false;
		}
	}
}
