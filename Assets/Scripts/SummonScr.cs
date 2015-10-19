using UnityEngine;
using System.Collections;

public class SummonScr : MonoBehaviour {

	public float growthSpeed = 0.003f;
	public float moveSpeed = 3;

	private Rigidbody2D body;
	private GameMgrScr mgr;
	private float currentSize = 0;
	private bool growthLimitReached = false;

	void Start()
	{
		body = GetComponent<Rigidbody2D> ();
		mgr = GameObject.Find ("GameMgr").GetComponent<GameMgrScr> ();

		transform.localScale = new Vector2 (0, 0);
	}

	// Update is called once per frame
	void Update () {
		if (!growthLimitReached) {
			currentSize += growthSpeed;
			if (currentSize > 1) {
				growthLimitReached = true;
				currentSize = 1;
				body.velocity = new Vector2 (moveSpeed, 0);
			}
		}

		transform.localScale = new Vector2 (currentSize, currentSize);
	}
}
