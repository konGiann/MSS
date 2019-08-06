using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class UnlockSkillManager : MonoBehaviour
{

    public GameObject[] AllSkills;
    public Transform Slot;
    public GameObject instance;

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

        ResetItemUnlockedState();
    }

    void Update()
    {
        /*We set in the editor all the skill prefabs. If a skill get unlocked (matching its level with player's level), 
		the panel is shown with the skill's prefab in its slot */
        foreach (var item in AllSkills)
        {
            var skill = item.GetComponent<DefensiveItems>();

            if (GameManager._instance.player.Level == skill.LevelRequired && skill.IsUnlocked == false)
            {

                OpenPanel();
                instance = Instantiate(item, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                instance.transform.SetParent(Slot, false);
                skill.IsUnlocked = true; // set it to true to avoid infinite instantiation
                SkillName.text = skill.Name;

                // Play sound
                SoundManager._instance.ItemUnlocked.Play();

                defInstance = instance.GetComponent<DefensiveItems>();

            }
        }

        if (Slot.childCount != 0)
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

    /// <summary>
    /// Close 'New Skill Unlocked' panel, only when you drag the item into the skillbar. 
    /// If you drag the item anywhere else, it will return to the panel, the panel will not close
    /// and will display the apropriate message to the user.
    /// </summary>
	public void ClosePanel()
    {
        if (Slot.childCount == 0 && Input.GetMouseButtonUp(0))
        {
            defInstance = instance.GetComponent<DefensiveItems>();

            GameObject[] skillBar = GameObject.FindGameObjectsWithTag("SkillBar");


            GameObject itemParent = defInstance.transform.parent.gameObject;

            bool found = false;
            foreach (var item in skillBar)
            {
                if (itemParent.transform.parent.gameObject == item && itemParent.tag != "Parent")
                {
                    anim.Play("NewSkillUnlockedPanelOut");
                    found = true;
                }
            }
            if (!found)
            {
                StartCoroutine(GUIManager._instance.ShowMessage("Drag the skill first to the skillbar.", 2));
            }
        }
    }

    void ResetItemUnlockedState()
    {
        foreach (var item in AllSkills)
        {
            var skill = item.GetComponent<DefensiveItems>();
            skill.IsUnlocked = false;
        }
    }
}
