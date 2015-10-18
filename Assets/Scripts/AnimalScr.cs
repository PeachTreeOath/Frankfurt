using UnityEngine;
using System.Collections;

public class AnimalScr : MonoBehaviour {

	private Rigidbody2D body;
	private GameMgrScr mgr;

	void Start()
	{
		body = GetComponent<Rigidbody2D> ();
		mgr = GameObject.Find ("GameMgr").GetComponent<GameMgrScr> ();
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "Dropping") {
			GetComponent<EdgeCollider2D> ().enabled = false;
			mgr.TriggerBlock(gameObject);
		}
	}

	void Update()
	{
		if (tag == "Dropping" && body.velocity.y == 0) {
			mgr.AllowBlock(gameObject);
			tag = "Dropped";
		}
	}
}
