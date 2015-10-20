using UnityEngine;
using System.Collections;

public class SummonScr : MonoBehaviour {

	public float growthSpeed = 0.003f;
	public float moveSpeed = 3;
	public bool isPlayer = false;
	public int HP;
	public float currentSize = 0;

	private Rigidbody2D body;
	private GameMgrScr mgr;
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

	void OnTriggerEnter2D (Collider2D collider)
	{
		SummonScr summon = collider.gameObject.GetComponent<SummonScr> ();
		if (summon == null) {
			return;
		}

		if (currentSize != 1 || summon.currentSize != 1) {
			return;
		}

		if (isPlayer) {
			int tempHP = HP;
			HP -= collider.gameObject.GetComponent<SummonScr>().HP;
			collider.gameObject.GetComponent<SummonScr>().HP -= tempHP;

			if(collider.gameObject.GetComponent<SummonScr>().HP <= 0)
			{
				Destroy (collider.gameObject);
			}

			if(HP <= 0)
			{
				Destroy (this.gameObject);
			}
		}
	}
}
