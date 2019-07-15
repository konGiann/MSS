using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelUpScreen : MonoBehaviour {

	public Text Name;
	public Text Level;
	public Text MaxEnergy;
	public Text MaxFear;
	public Text PointsToSpend;

	public Button MaxFearBtn;
	public Button MaxEnergyBtn;

	bool isActivated;

	void Start()
	{
		MaxFearBtn.onClick.AddListener (AddFear);
		MaxEnergyBtn.onClick.AddListener (AddEnergy);
	}

	void Update()
	{
		Name.text = GameManager._instance.player.Name;
		Level.text = "Lvl " + GameManager._instance.player.Level.ToString ();
		MaxEnergy.text = GameManager._instance.player.MaximumEnergy.ToString ();
		MaxFear.text = GameManager._instance.player.MaximumFear.ToString ();
		PointsToSpend.text = GameManager._instance.player.PointsToSpend.ToString ();
	}

	void AddFear()
	{
		if(GameManager._instance.player.PointsToSpend > 0)
		{
			GameManager._instance.player.PointsToSpend -= 1;
			GameManager._instance.player.MaximumFear += 1;
		}
		else
		{
			GUIManager._instance.GameMessages.text = "No points to spend :(";
			StartCoroutine (GUIManager._instance.DeleteGameMessageTextAfterSeconds (2));
		}
	}

	void AddEnergy()
	{
		if(GameManager._instance.player.PointsToSpend > 0)
		{
			GameManager._instance.player.PointsToSpend -= 1;
			GameManager._instance.player.MaximumEnergy += 1;
		}
		else
		{
			GUIManager._instance.GameMessages.text = "No points to spend :(";
			StartCoroutine (GUIManager._instance.DeleteGameMessageTextAfterSeconds (2));
		}
	}

}
