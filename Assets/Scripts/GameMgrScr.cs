using UnityEngine;
using System.Collections;

public class GameMgrScr : MonoBehaviour {

	private bool pileStarted = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool AllowBlock()
	{
		if (!pileStarted) {
			pileStarted = true;
			return true;
		}
		GameObject[] objs = GameObject.FindGameObjectsWithTag ("Dropped");
		foreach (GameObject obj in objs) {
			Destroy(obj);
		}
		return false;
	}
}
