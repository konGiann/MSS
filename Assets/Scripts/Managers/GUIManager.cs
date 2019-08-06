using System;
using System.Collections;
 using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {
	#region Variables
	// All the top screen stats
	public Text PlayerName;
	public Text PlayerLevel;
	public Text Fear;
	public Text Energy;
	public Text ExperiencePoints;
	public Text GameMessages;
	public Text EnemiesRemaining;
	public Text WaveLevel;
	public Text WaveCountDown;
	public Button ReadyButton;
	public Text KillsText;

	// Levelup Screen
	public GameObject levelUpScreen;
	public bool IsLevelUpScreenActive;

	// AllSkills panel
	public GameObject SkillSelectionPanel;
	private Animator animAllSkils;
	public bool ShowHideAllSKills;

	// Crafting panel
	public GameObject CraftingPanel;
	private Animator animCrafting;
	public bool isCraftingPanelActive;

    // Coroutine chekers
    public bool CoroutineIsRunning = false;

	#endregion

	public static GUIManager _instance = null;

	void Awake()
	{
		if (_instance == null)
			_instance = this;
		else if(_instance != null)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);

		// Hide level up screen on game start
		levelUpScreen.SetActive (false);

	}

	void Start()
	{
		ReadyButton.onClick.AddListener (ReadyForWave);

		// Get AllSKills panel Animator
		animAllSkils = SkillSelectionPanel.GetComponent <Animator>();

		// Get Crafting panel Animator
		animCrafting = CraftingPanel.GetComponent <Animator>();

		// Hide Crafting panel on game start
		isCraftingPanelActive = false;

		// Hide AllSkills on game start
		ShowHideAllSKills = false;

        Screen.SetResolution(1920, 1080, true);

	}

	void Update () 
	{
		// Always show top screen player stats
		ShowPlayerStats ();

		CountRemainingEnemies ();

		ShowHideLevelUpScreen ();

		ShowHideCraftingPanel ();

		ShowHideSkillsPanel ();

		// Countdown between waves
		CountDown ();
        
	}

	#region Methods

	// Shot top screen player stats
	void ShowPlayerStats ()
	{
		PlayerName.text = GameManager._instance.player.Name;
		PlayerLevel.text = "LVL " + GameManager._instance.player.Level.ToString ();
		Fear.text = "F " + Convert.ToInt32 (GameManager._instance.player.CurrentFear).ToString () + "/" + GameManager._instance.player.MaximumFear.ToString ();
		Energy.text = "E " + GameManager._instance.player.CurrentEnergy.ToString () + "/" + GameManager._instance.player.MaximumEnergy.ToString();
		ExperiencePoints.text = "XP " + GameManager._instance.player.CurrentExperiencePoints.ToString () + "/" + GameManager._instance.player.ExperiencePointsNeeded.ToString ();
		WaveLevel.text = "Wave: " + Mathf.Round (GameManager._instance.WaveLevel).ToString ();

        Fear.color = Color.white;

		// Warn the player if his fear lebel is above 70% of his total fear points
		if (GameManager._instance.player.CurrentFear > GameManager._instance.player.MaximumFear * 0.7)
		{
			StartCoroutine ("FlashFearText");

		}

		KillsText.text = GameManager._instance.player.creaturesKilled.ToString ();

	}

	// COROUTINE Makes the warning flash with red and white colors every half a second
	IEnumerator FlashFearText()
	{
		while (GameManager._instance.player.CurrentFear > GameManager._instance.player.MaximumFear * 0.7) 
		{
			yield return new WaitForSeconds (0.5f);
			Fear.color = Color.red;
			yield return new WaitForSeconds (0.5f);
			Fear.color = Color.white;
	   }
	}

	// Show enemies remaining in top bar stats
	void CountRemainingEnemies()
	{
		EnemiesRemaining.text = "Enemies : " + GameManager._instance.creaturesInScene.Length.ToString ();


	}

    /// <summary>
    /// Displays a message for a fixed amound of seconds
    /// </summary>
    /// <param name="message">Message to be displayed</param>
    /// <param name="timeToStayAlive">Seconds to be displayed</param>
    /// <returns></returns>
	public IEnumerator ShowMessage(string message, int timeToStayAlive, Color? color = null)
    {
        CoroutineIsRunning = true;
        GameObject parentMessagePanel = GameMessages.transform.parent.gameObject;
        parentMessagePanel.SetActive(true);
        if(color == null)
        {
            GameMessages.color = Color.white;
        }
        else
        {
            GameMessages.color = (Color)color;
        }
        GameMessages.text = message;
        yield return new WaitForSeconds(timeToStayAlive);
        parentMessagePanel.SetActive(false);
        CoroutineIsRunning = false;
        GameMessages.text = string.Empty;        
    }

	void ShowHideSkillsPanel()
	{
		if (GameManager._instance.currentState == GameManager.GameState.PREPARATION) 
		{
            SkillSelectionPanel.SetActive(true);
			if (Input.GetKeyDown (KeyCode.K)) 
			{
				ShowHideAllSKills = !ShowHideAllSKills;

				if(ShowHideAllSKills == true)
				{
					animAllSkils.enabled = true;
					animAllSkils.Play ("AllSkillsSlideIn");
				}
				else
				{
					animAllSkils.Play ("AllSkillsSlideOut");
				}
			}
		} 
		else if (GameManager._instance.currentState != GameManager.GameState.PREPARATION)
			SkillSelectionPanel.SetActive (false);
	}

	void ShowHideCraftingPanel()
	{
		if(Input.GetKeyDown (KeyCode.C))
		{			
			isCraftingPanelActive = !isCraftingPanelActive;
			if(isCraftingPanelActive == true)
			{
				animCrafting.enabled = true;
				animCrafting.Play ("CraftingPanelIn");
			}
			else
			{
				animCrafting.Play ("CraftingPanelOut");
			}
		}
	}

	void ShowHideLevelUpScreen()
	{
		if(Input.GetKeyDown (KeyCode.L))
		{
			levelUpScreen.SetActive (!IsLevelUpScreenActive);
			IsLevelUpScreenActive = !IsLevelUpScreenActive;
		}
	}

	public void ReadyForWave()
	{
		GameManager._instance.currentState = GameManager.GameState.SPAWNWAVE;
		ReadyButton.gameObject.SetActive (false);
		GameManager._instance.WaveLevel += 1;

	}

	public void CountDown()
	{
		
		if (GameManager._instance.currentState == GameManager.GameState.PREPARATION)
			WaveLevel.text = string.Empty;

		if (GameManager._instance.currentState == GameManager.GameState.SPAWNWAVE)
		{
			WaveCountDown.text = Mathf.Round (GameManager._instance.waveCountDown).ToString (); 		
		}

		if (GameManager._instance.waveCountDown <= -0.5)
		{
			WaveCountDown.text = string.Empty;		

		}


	}

	#endregion


}
