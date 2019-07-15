using UnityEngine;
using UnityEngine.UI;

public class ShotInfoText : MonoBehaviour {
    public Text Health;
    public Text DamagePerSec;
    public Text InitialBonusDamage;

    public GameObject InfoText;

    public DefensiveItems defItem;


	// Use this for initialization
	void Start ()
    {
        defItem = GetComponentInParent<DefensiveItems>();
        InfoText.SetActive(false);
	}


    void OnMouseEnter()
    {
        InfoText.SetActive(true);
        ShowPopUpStats();
    }

    void OnMouseExit()
    {
        InfoText.SetActive(false);
    }

    public void ShowPopUpStats()
    {
		Health.text = "H: " + defItem.CurrentHitPoints.ToString ("0") + "/" + defItem.MaximumHitPoints.ToString ("0");
        DamagePerSec.text = "DPS: " + defItem.ItemDamagePerSec.ToString("0.00");
		InitialBonusDamage.text = "IBD: " + defItem.InitialBonusDamage.ToString ("0.00");
    }

}
