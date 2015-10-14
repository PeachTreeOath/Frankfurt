using UnityEngine;
using System.Collections;

public class CraneScr : MonoBehaviour {

	public int boundL;
	public int boundR;
	public float speed;

	private Rigidbody2D body;
	private GameObject pile;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
		body.velocity = new Vector2 (speed, 0);

		pile = GameObject.Find ("Pile");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Jump")) {
			GameObject animalObj = GameObject.Find("DonkeyPF");
			Rigidbody2D animalBody = animalObj.GetComponent<Rigidbody2D>();
			animalBody.isKinematic = false;
			animalObj.transform.parent = pile.transform;
		}

		if (transform.position.x > boundR || transform.position.x < boundL) {
			speed = -speed;

		}
		body.velocity = new Vector2 (speed, 0);
	}

}
