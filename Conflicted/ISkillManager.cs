using UnityEngine;

public class ISkillManager : MonoBehaviour {

	//ISkill salt;
	//ISkill water;
	//ISkill fire;



	#region Singleton
	public static ISkillManager _instance = null;

	void Awake()
	{
		if (_instance == null)
			_instance = this;
		else if (_instance != null)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);
	}
	#endregion

	void Start () 
	{
		//salt = new ISkill ("Salt", 1);
		//water = new ISkill ("Water", 1);

		//fire = new ISkill ("Fire", 3);

	

	}
	
	// Update is called once per frame
	void Update () 
	{
        //ShowSkills ();
        

	}

	//void ShowSkills()
	//{
	//	foreach (var skill in GameManager._instance.player.SkillBook)
	//	{
	//		Debug.Log (skill.Name + " | " + "Skill Level : " + skill.LevelRequired);
	//	}

	//}


}
