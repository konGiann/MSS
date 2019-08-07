using UnityEngine;
using System.Collections;

public class MonsterMovement : MonoBehaviour {

	private Rigidbody2D rb;
	public float speed;
	void Awake() 
	{
		rb = GetComponent <Rigidbody2D> ();
	}

	void Start () {
	
	}
	
	void FixedUpdate () 
	{
		rb.velocity = new Vector2 (0.1f * speed, 0);
	}

    //void OnTriggerEnter2D(Collider2D floor)
    //{
    //    floor.tag = "FloorOccupied";
    //}
}
