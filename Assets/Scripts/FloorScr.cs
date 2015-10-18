using UnityEngine;
using System.Collections;

public class FloorScr : MonoBehaviour {

	private GameMgrScr mgr;

	void Start()
	{
		mgr = GameObject.Find ("GameMgr").GetComponent<GameMgrScr> ();
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "Dropping") {
			mgr.AllowBlock(collider.gameObject);
			collider.gameObject.tag = "Dropped";
		}
	}
}
