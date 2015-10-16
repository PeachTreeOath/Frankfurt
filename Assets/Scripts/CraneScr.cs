using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CraneScr : MonoBehaviour
{

	public int boundL;
	public int boundR;
	public float speed;
	public float generationTime;
	public GameObject animalPF1;
	public GameObject animalPF2;
	public GameObject animalPF3;
	public GameObject animalPF4;
	private Rigidbody2D body;
	private GameObject pile;
	private List<GameObject> animalPFs;
	private float currSpeed;

	// Use this for initialization
	void Start ()
	{
		currSpeed = speed;
		body = GetComponent<Rigidbody2D> ();
		body.velocity = new Vector2 (currSpeed, 0);

		pile = GameObject.Find ("Pile");
		animalPFs = new List<GameObject> ();
		animalPFs.Add (animalPF1);
		animalPFs.Add (animalPF2);
		animalPFs.Add (animalPF3);
		animalPFs.Add (animalPF4);

		GenerateChild ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		GameObject payload = GameObject.FindGameObjectWithTag ("Undropped");
		if (Input.GetButtonDown ("Jump") && payload != null) {
			Rigidbody2D animalBody = payload.GetComponent<Rigidbody2D> ();
			animalBody.isKinematic = false;
			payload.transform.parent = pile.transform;
			payload.tag = "Dropping";

			Invoke ("GenerateChild", generationTime);
		}

		if (transform.position.x > boundR) {
			currSpeed = -speed;
		} else if (transform.position.x < boundL) {
			currSpeed = speed;
		}
		body.velocity = new Vector2 (currSpeed, 0);
	}

	private void GenerateChild ()
	{
		int index = Random.Range (0, 4);
		Debug.Log (index);
		GameObject pf = animalPFs [index];
		GameObject newObj = (GameObject)Instantiate (pf, new Vector3 (0, 0, 0), Quaternion.identity);
		Transform newTransform = newObj.GetComponent<Transform> ();
		newTransform.parent = transform;
		newTransform.localPosition = new Vector2 (0, 0);
	}
}
