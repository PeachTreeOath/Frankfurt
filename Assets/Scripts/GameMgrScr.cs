using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameMgrScr : MonoBehaviour
{

	private GameObject summonParent;
	private GameObject topBlock;
	private bool topBlockTriggered = true;
	private int blockNum = 0;
	private int stackHeight = 0;
	private Button summonButton;
	private CraneScr crane;
	private Camera cam;

	public List<GameObject>  summonPFs;
	public GameObject summon1;
	public GameObject summon2;
	public GameObject summon3;
	public GameObject summon4;
	public GameObject summon5;
	public GameObject summon6;
	public GameObject summon7;
	public GameObject summon8;
	public GameObject summon9;

	public float summonPointX;
	public float summonPointY;

	// Use this for initialization
	void Start ()
	{
		summonParent = GameObject.Find ("Summons");		
		summonButton = GameObject.Find ("SummonButton").GetComponent<Button> ();
		crane = GameObject.Find ("Crane").GetComponent<CraneScr> ();
		cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();

		summonPFs = new List<GameObject> ();
		summonPFs.Add (summon1);
		summonPFs.Add (summon2);
		summonPFs.Add (summon3);
		summonPFs.Add (summon4);
		summonPFs.Add (summon5);
		summonPFs.Add (summon6);
		summonPFs.Add (summon7);
		summonPFs.Add (summon8);
		summonPFs.Add (summon9);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (GameObject.FindGameObjectWithTag ("Dropping")) {
			summonButton.interactable = false;
		}
		else if (stackHeight > 0) {
			summonButton.interactable = true;
			crane.GenerateChild();
			crane.SetSpeed(stackHeight);
		}
	}

	// Determine whether to allow subsequent block based on trigger
	public bool AllowBlock (GameObject block)
	{
		if (topBlockTriggered) {
			if (topBlock != null && block.transform.position.y > topBlock.transform.position.y) {
				SetTopBlock (block);
				return true;
			}
		}

		DestroyBlocks ();
		SetTopBlock (block);
		return false;
	}

	// Trigger when block is laid on top (should only apply to topmost block)
	public void TriggerBlock (GameObject block)
	{
		if (block.name == topBlock.name) {
			topBlockTriggered = true;
		}
	}

	public string AssignName ()
	{
		blockNum++;
		return "Block_" + blockNum;
	}

	public void PlayerSummon ()
	{
		if (!summonButton.interactable) {
			return;
		}

		GameObject summonPF = summonPFs[stackHeight-1];
		GameObject newSummon = (GameObject)GameObject.Instantiate (summonPF, new Vector3 (summonPointX+summonPF.GetComponent<SpriteRenderer>().bounds.size.x/2, summonPointY+summonPF.GetComponent<SpriteRenderer>().bounds.size.y/2, 0), Quaternion.identity);
		newSummon.transform.parent = summonParent.transform;

		PlayBlocks ();
		DisableCrane ();
	}

	// Set the top block
	private void SetTopBlock (GameObject block)
	{
		topBlock = block;
		topBlockTriggered = false;
		stackHeight++;
		crane.SetSpeed(stackHeight);
		summonButton.interactable = true;
		summonButton.GetComponentInChildren<Text>().text = "SUMMON LVL " + stackHeight;
		cam.transform.position = new Vector3 (cam.transform.position.x, cam.transform.position.y + block.GetComponent<SpriteRenderer>().bounds.size.y, cam.transform.position.z);
		crane.transform.position = new Vector3 (crane.transform.position.x, crane.transform.position.y + block.GetComponent<SpriteRenderer>().bounds.size.y, crane.transform.position.z);
	}
	
	private void PlayBlocks ()
	{
		GameObject[] objs = GameObject.FindGameObjectsWithTag ("Dropped");
		foreach (GameObject obj in objs) {
			AnimalScr animal = obj.GetComponent<AnimalScr> ();
			animal.PlayNotes ();
		}

		ResetStage ();
	}

	private void DestroyBlocks ()
	{
		GameObject[] objs = GameObject.FindGameObjectsWithTag ("Dropped");
		foreach (GameObject obj in objs) {
			Destroy (obj);
		}

		ResetStage ();
	}

	private void ResetStage ()
	{
		topBlock = null;
		topBlockTriggered = false;
		stackHeight = 0;
		crane.SetSpeed(stackHeight);
		summonButton.interactable = false;
		summonButton.GetComponentInChildren<Text>().text = "SUMMON";

		cam.transform.position = new Vector3 (0, 0, cam.transform.position.z);
		crane.transform.position = new Vector3 (0, 3, crane.transform.position.z);
	}

	private void DisableCrane ()
	{
		crane.Disable ();
		Invoke ("EnableCrane", 6f);
	}

	private void EnableCrane ()
	{
		crane.Enable ();
	}
}
