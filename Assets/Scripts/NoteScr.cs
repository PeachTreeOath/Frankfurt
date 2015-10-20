using UnityEngine;
using System.Collections;

public class NoteScr : MonoBehaviour {

	public float waveDeviation;
	public float waveSpeed;
	public float speed;

	private Rigidbody2D body;
	public float currWaveSpeed;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();	
		body.velocity = new Vector2 (speed, 0);

		currWaveSpeed = waveSpeed * Random.Range(-1f, 1f);

		Invoke ("DestroySelf", 3f);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate()
	{
		body.AddForce (new Vector2 (0, currWaveSpeed));

		if (body.velocity.y > waveDeviation) {
			currWaveSpeed = -waveSpeed;
		} else if (body.velocity.y < -waveDeviation) {
			currWaveSpeed = waveSpeed;
		}
	}

	private void DestroySelf()
	{
		Destroy (this.gameObject);
	}
}
