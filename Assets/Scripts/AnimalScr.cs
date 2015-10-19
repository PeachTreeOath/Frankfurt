using UnityEngine;
using System.Collections;

public class AnimalScr : MonoBehaviour
{
	public GameObject note;

	private Rigidbody2D body;
	private GameMgrScr mgr;
	private GameObject notes;

	void Start ()
	{
		body = GetComponent<Rigidbody2D> ();
		mgr = GameObject.Find ("GameMgr").GetComponent<GameMgrScr> ();
		notes = GameObject.Find ("Notes");
	}

	void OnTriggerEnter2D (Collider2D collider)
	{
		if (collider.gameObject.tag == "Dropping") {
			GetComponent<EdgeCollider2D> ().enabled = false;
			mgr.TriggerBlock (gameObject);
		}
	}

	void Update ()
	{
		if (tag == "Dropping" && body.velocity.y == 0) {
			mgr.AllowBlock (gameObject);
			tag = "Dropped";
		}
	}

	public void PlayNotes ()
	{
		Invoke ("PlayNote", Random.Range (0f, 1f));
		Invoke ("PlayNote", Random.Range (1f, 2f));
		Invoke ("PlayNote", Random.Range (2f, 2.999f));
		Invoke ("DestroySelf", 6f);
	}

	private void PlayNote ()
	{
		Vector3 pos = transform.position;
		GameObject newNote = (GameObject)Instantiate (note, new Vector3 (pos.x, pos.y, 0), Quaternion.identity);
		newNote.transform.parent = notes.transform;
	}

	private void DestroySelf()
	{
		Destroy (this.gameObject);
	}
}
