using UnityEngine;
using UnityEngine.UI;

public class CraftItem : MonoBehaviour 
{
	public DefensiveItems defItem;

	public Text AttackValue;
	public Text AttackCost;

	public Text HealthValue;
	public Text HealthCost;

	public Text ItemBuyCost;
	public Text ItemName;


	void Update()
	{
		defItem = GameObject.Find (gameObject.name).GetComponent<DefensiveItems>();
		ShowAttackStats();
		ShowHealthStats();
		ShowItemInfo();
	}


	public void CraftItemOnClick()
	{
		
		if (GameManager._instance.player.CurrentEnergy >= defItem.GetComponent <DefensiveItems>().EnergyRequiredToCraft)
		{
			defItem.GetComponent <DefensiveItems>().NumberAvailable += 1;
			GameManager._instance.player.CurrentEnergy -= defItem.EnergyRequiredToCraft;
		}

	}
	
	public void ShowAttackStats()
	{
		if(defItem != null)
		{
			AttackValue.text = defItem.ItemDamagePerSec.ToString ("0.00");
			AttackCost.text = defItem.AttackUpgradeCost.ToString();
		}
	}

	public void ShowHealthStats()
	{
		if (defItem != null)
		{
			HealthValue.text = defItem.MaximumHitPoints.ToString();
			HealthCost.text = defItem.HealthUpgradeCost.ToString();
		}
		
	}

	public void ShowItemInfo()
	{
        if (defItem != null)
        {
            ItemBuyCost.text = defItem.EnergyRequiredToCraft.ToString();
            ItemName.text = defItem.name;
        }
            
	}

	public void BuyItem()
	{
		if(GameManager._instance.player.CurrentEnergy >= defItem.EnergyRequiredToCraft)
		{
			defItem.NumberAvailable += 1; 
			GameManager._instance.player.CurrentEnergy -= defItem.EnergyRequiredToCraft;
		}
	}

	public void UpgradeHealth()
	{
		if (GameManager._instance.player.CurrentEnergy >= defItem.HealthUpgradeCost)
		{
			defItem.MaximumHitPoints += (int)(defItem.MaximumHitPoints * 0.20); // How much to upgrade
            defItem.CurrentHitPoints = defItem.MaximumHitPoints; // Also upgrade current HP to match MAX HP
			GameManager._instance.player.CurrentEnergy -= defItem.HealthUpgradeCost; // Subtract energy from player
			defItem.HealthUpgradeCost += (int)(defItem.HealthUpgradeCost * 0.20 + 1); // How much the next upgrade will cost
			defItem.EnergyRequiredToCraft += (int)(defItem.EnergyRequiredToCraft * 0.15 + 1); // How much the Item will cost after the upgrade
		}
	}

	public void UpgradeAttack()
	{
		if(GameManager._instance.player.CurrentEnergy >= defItem.AttackUpgradeCost)
		{
			defItem.ItemDamagePerSec += (defItem.ItemDamagePerSec * 0.2f); // How much to upgrade
			GameManager._instance.player.CurrentEnergy -= defItem.AttackUpgradeCost; // Subtract energy from player
			defItem.AttackUpgradeCost += (int)(defItem.AttackUpgradeCost * 0.20 + 1.5); // How much the next upgrade will cost
			defItem.EnergyRequiredToCraft += (int)(defItem.EnergyRequiredToCraft * 0.15 + 1); // How much the Item will cost after the upgrade

            
        }
	}

	public void ShowAttackValuePopUp()
	{
		float value = defItem.ItemDamagePerSec += (defItem.ItemDamagePerSec * 0.2f); 
		Debug.Log (value);
	}

	void OnMouseOver()
	{
		ShowAttackValuePopUp ();
	}

}
