using UnityEngine;
using System.Collections;

public class AnimalScr : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "Dropping") {
			collider.gameObject.tag = "Dropped";
		}
	}
}
