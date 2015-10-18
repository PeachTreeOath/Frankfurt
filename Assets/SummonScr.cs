using UnityEngine;
using System.Collections;

public class SummonScr : MonoBehaviour {

	private Rigidbody2D body;
	private GameMgrScr mgr;
	
	void Start()
	{
		body = GetComponent<Rigidbody2D> ();
		mgr = GameObject.Find ("GameMgr").GetComponent<GameMgrScr> ();

		body.velocity = new Vector2 (3, 0);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
