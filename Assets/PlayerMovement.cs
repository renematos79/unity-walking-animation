using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	private enum Orientation {Left, Right};

	public float Speed = 2.5f;
	public float JumpSpeed = 5f;
	private Animator anim;
	private Orientation State;
	private Rigidbody2D MyRigibody;


	// Use this for initialization
	void Start () {
		this.anim = GetComponent<Animator>();
		this.MyRigibody = GetComponent<Rigidbody2D>();
		this.State = Orientation.Right;
	}

	private void TurnRight(){
		anim.SetBool ("idle-right", true);
		anim.SetBool ("walk-right", false);
		anim.SetBool ("idle-left", false);
		anim.SetBool ("walk-left", false);
	}

	private void WalkRight(){
		this.gameObject.transform.Translate (new Vector2 (this.Speed * Time.deltaTime, 0));
		anim.SetBool ("idle-right", false);
		anim.SetBool ("walk-right", true);
		anim.SetBool ("idle-left", false);
		anim.SetBool ("walk-left", false);
	}

	private void TurnLeft(){
		anim.SetBool ("idle-right", false);
		anim.SetBool ("walk-right", false);
		anim.SetBool ("idle-left", true);
		anim.SetBool ("walk-left", false);
	}

	private void WalkLeft(){
		this.gameObject.transform.Translate (new Vector2 (-1 * this.Speed * Time.deltaTime, 0));
		anim.SetBool ("idle-right", false);
		anim.SetBool ("walk-right", false);
		anim.SetBool ("idle-left", false);
		anim.SetBool ("walk-left", true);
	}

	private void Jump(){
		this.MyRigibody.AddForce(Vector2.up * JumpSpeed, ForceMode2D.Force);
	}

	// Update is called once per frame
	void Update () {
		var isJump = Input.GetKeyDown(KeyCode.Space);
		var idle = true;

		// direita
		if (Input.GetKey (KeyCode.RightArrow)) {
			if (this.State == Orientation.Left) { 
				this.TurnRight ();
			} else {
				this.WalkRight ();
			}
			this.State = Orientation.Right;
			idle = false;
		} 

		// esquerda
		if (Input.GetKey (KeyCode.LeftArrow)) {
			if (this.State == Orientation.Right) { 
				this.TurnLeft ();	
			} else {
				this.WalkLeft ();
			}
			this.State = Orientation.Left;
			idle = false;
		}

		if (idle) {
			if (this.State == Orientation.Left) { 
				this.TurnLeft ();
			} else {
				this.TurnRight ();
			}
		}

		// pulo
		if (isJump) this.Jump ();
	}
}