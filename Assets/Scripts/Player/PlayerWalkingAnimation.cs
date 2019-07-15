using UnityEngine;
using System.Collections;

public class PlayerWalkingAnimation : MonoBehaviour {
	private Animator anim;
	// Use this for initialization
	void Start ()
	{
		anim = GetComponent <Animator> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		PlayerMovementAnimation ();
	}

	void PlayerMovementAnimation ()
	{
		if (Input.GetKey (KeyCode.D)) {
			anim.SetBool ("WalkRight", true);
			anim.SetBool ("WalkLeft", false);
			anim.SetBool ("WalkUp", false);
			anim.SetBool ("WalkDown", false);
			anim.SetBool ("Idle", false);
		}
		if (Input.GetKey (KeyCode.A)) {
			anim.SetBool ("WalkRight", false);
			anim.SetBool ("WalkLeft", true);
			anim.SetBool ("WalkUp", false);
			anim.SetBool ("WalkDown", false);
			anim.SetBool ("Idle", false);
		}
		if (Input.GetKey (KeyCode.W)) {
			anim.SetBool ("WalkRight", false);
			anim.SetBool ("WalkLeft", false);
			anim.SetBool ("WalkUp", true);
			anim.SetBool ("WalkDown", false);
			anim.SetBool ("Idle", false);
		}
		if (Input.GetKey (KeyCode.S)) {
			anim.SetBool ("WalkRight", false);
			anim.SetBool ("WalkLeft", false);
			anim.SetBool ("WalkUp", false);
			anim.SetBool ("WalkDown", true);
			anim.SetBool ("Idle", false);
		}
		if (!Input.anyKey) {
			anim.SetBool ("WalkRight", false);
			anim.SetBool ("WalkLeft", false);
			anim.SetBool ("WalkUp", false);
			anim.SetBool ("WalkDown", false);
			anim.SetBool ("Idle", true);
		}
	}
}
