using UnityEngine;
using System.Collections;

public class FloorScr : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "Dropping") {
			if(GameObject.Find("GameMgr").GetComponent<GameMgrScr>().AllowBlock())
			{
				collider.gameObject.tag = "Dropped";
			}
		}
	}
}
