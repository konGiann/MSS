using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public PlayerStats player;
    ICreature[] vampyrsInScene;
    public GameObject bestiary;
    GameObject bestiaryPanel;
    public bool IsBestiaryActive;

	// Make it Signleton
	public static GameManager _instance = null;
	void Awake()
	{
		if (_instance == null)
			_instance = this;
		else if (_instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);		
        	
		// Instantiate the player
		player = new PlayerStats ("Dean", 1, 20, 10, 0, 25);

        bestiary = GameObject.FindGameObjectWithTag("Bestiary");
        
		vampyrsInScene = new Vampyr[4];
		vampyrsInScene = Object.FindObjectsOfType <ICreature>();

	}

	void Start ()
	{
		GameManager._instance.player.CreatureBestiary = new List<ICreature> ();
		foreach (var creature in vampyrsInScene) 
		{
			if(ICreature.AddedToBestiary == true)
			{
				GameManager._instance.player.AddCreatureToBestiary (creature);
				ICreature.AddedToBestiary = false;
			}


		}

		foreach (var creature in GameManager._instance.player.CreatureBestiary) 
		{
			Debug.Log (creature.Name + " | " + creature.Description);
		}
	}

	void Update()
	{
        ShowHideBestiary();
    }

    void ShowHideBestiary()
    {
        
        if (Input.GetKeyDown(KeyCode.B))
        {
            bestiary.SetActive(!IsBestiaryActive);
            IsBestiaryActive = !IsBestiaryActive;
        }
    }
	


}
