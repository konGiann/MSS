using UnityEngine;
using System.Collections.Generic;


public class BestiaryCreatures : MonoBehaviour {

    public Transform vampyrPref;
	public Transform gargoylePref;

	GameObject myVampyr;
	GameObject myGargoyle;

	public List<GameObject> creatures;

	bool isVampyrInstantiated;
	bool isGargoyleInstantiated;
    
	void Awake()
	{
		creatures = new List<GameObject>();
	}

    void Start ()
    {

    
	}
	
	// Update is called once per frame
	void Update ()
    {
		
		if (!isVampyrInstantiated)
        {
            if (GameManager._instance.player.CreatureBestiary.Find(creature => creature.Name == "Vampyr"))
            {
				isVampyrInstantiated = true;
                myVampyr =  Instantiate(vampyrPref, new Vector3(-60, 100, 0), Quaternion.identity) as GameObject;
				myVampyr = GameObject.Find ("BestiaryVampyr(Clone)");
				myVampyr.transform.SetParent (gameObject.transform, false);
				creatures.Add (myVampyr);
							               
            }
			            
        }

		if (!isGargoyleInstantiated)
		{
			if (GameManager._instance.player.CreatureBestiary.Find(creature => creature.Name == "Gargoyle"))
			{
				isGargoyleInstantiated = true;
				myGargoyle =  Instantiate(gargoylePref, new Vector3(-60, 35, 0), Quaternion.identity) as GameObject;
				myGargoyle = GameObject.Find ("BestiaryGargoyle(Clone)");
				myGargoyle.transform.SetParent (gameObject.transform, false);
				creatures.Add (myGargoyle);

			}

		}


	}
}
