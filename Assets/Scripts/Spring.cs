using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour {

	public float force = 15.0f;

	private Animator animator;

	private CharSpring Sprung
	{
		get { return (CharSpring)animator.GetInteger("Sprung"); }
		set { animator.SetInteger("Sprung", (int)value); }
	}




	void Start () {
		
		animator = GetComponent<Animator> ();
	}
	

	void Update () {
		
	}

	void FixedUpdate () {
		if(Sprung == CharSpring.Sprung) StartCoroutine ("Idle");
	}

	public void OnTriggerEnter2D(Collider2D collider)
	{
		Sprung = CharSpring.Sprung;

		Controller player = collider.GetComponent<Controller> ();

		player.ReceiveForce (force);
	}

	IEnumerator Idle(){
		yield return new WaitForSeconds (1);
		Sprung = CharSpring.Idle;
	}

}

public enum CharSpring
{
	Idle,
	Sprung
}

