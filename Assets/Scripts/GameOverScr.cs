using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverScr : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (!GameObject.Find ("WinTracker").GetComponent<WinScr> ().win) {
			GameObject.Find("WinText").GetComponent<Text>().text = "YOU LOSE";
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Reset()
	{
		Application.LoadLevel ("Game");
	}
}
