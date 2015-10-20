using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CraneScr : MonoBehaviour
{
	public int boundL;
	public int boundR;
	public float origSpeed = 8;
	public float speedMultiplier = 1;
	public float generationTime;
	public GameObject animalPF1;
	public GameObject animalPF2;
	public GameObject animalPF3;
	public GameObject animalPF4;

	private float speed;
	private Rigidbody2D body;
	private GameObject pile;
	private List<GameObject> animalPFs;
	private float currSpeed;
	private GameMgrScr mgr;
	private bool disabled = false;

	// Use this for initialization
	void Start ()
	{
		mgr = GameObject.Find ("GameMgr").GetComponent<GameMgrScr> ();

		currSpeed = origSpeed;
		speed = origSpeed;
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
		if (Input.GetButtonDown ("Jump") && !disabled && payload != null) {
			Rigidbody2D animalBody = payload.GetComponent<Rigidbody2D> ();
			animalBody.isKinematic = false;
			payload.transform.parent = pile.transform;
			payload.tag = "Dropping";
			payload.name = mgr.AssignName();
		}

		if (transform.position.x > boundR) {
			currSpeed = -speed;
		} else if (transform.position.x < boundL) {
			currSpeed = speed;
		}
		body.velocity = new Vector2 (currSpeed, 0);
	}

	public void GenerateChild ()
	{
		if (GetComponentInChildren<AnimalScr> () != null) {
			return;
		}

		int index = Random.Range (0, 4);
		GameObject pf = animalPFs [index];
		GameObject newObj = (GameObject)Instantiate (pf, new Vector3 (0, 0, 0), Quaternion.identity);
		Transform newTransform = newObj.GetComponent<Transform> ();
		newTransform.parent = transform;
		newTransform.localPosition = new Vector2 (0, 0);
		Rigidbody2D newBody = newObj.GetComponent<Rigidbody2D> ();
		newBody.velocity = new Vector2(0,-1);
	}

	public void Disable()
	{
		disabled = true;
		GetComponentInChildren<BoxCollider2D> ().enabled = false;
		GameObject.FindGameObjectWithTag ("Undropped").GetComponent<SpriteRenderer>().sortingLayerName = "Hidden";
	}

	public void Enable()
	{
		disabled = false;
		GetComponentInChildren<BoxCollider2D> ().enabled = true;
		GameObject.FindGameObjectWithTag ("Undropped").GetComponent<SpriteRenderer>().sortingLayerName = "Default";
	}

	public void SetSpeed(int level)
	{
		speed = origSpeed + speedMultiplier * level;
	}
}
