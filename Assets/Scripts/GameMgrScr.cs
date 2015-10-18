using UnityEngine;
using System.Collections;

public class GameMgrScr : MonoBehaviour {
	
	private GameObject topBlock;
	private bool topBlockTriggered = true;
	private int blockNum = 0;

	public GameObject summon1;
	public int summonPointX;
	public int summonPointY;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Determine whether to allow subsequent block based on trigger
	public bool AllowBlock(GameObject block)
	{
		if (topBlockTriggered) {
			if(topBlock != null && block.transform.position.y > topBlock.transform.position.y)
			{
				SetTopBlock (block);
				return true;
			}
		}

		DestroyBlocks ();
		SetTopBlock (block);
		return false;
	}

	// Trigger when block is laid on top (should only apply to topmost block)
	public void TriggerBlock(GameObject block)
	{
		if (block.name == topBlock.name) {
			topBlockTriggered = true;
		}
	}

	public string AssignName()
	{
		blockNum++;
		return "Block_" + blockNum;
	}

	public void PlayerSummon()
	{
		GameObject.Instantiate (summon1, new Vector3 (summonPointX, summonPointY, 0), Quaternion.identity);
		DestroyBlocks ();
	}

	// Set the top block
	private void SetTopBlock(GameObject block)
	{
		topBlock = block;
		topBlockTriggered = false;
	}

	private void DestroyBlocks()
	{
		GameObject[] objs = GameObject.FindGameObjectsWithTag ("Dropped");
		foreach (GameObject obj in objs) {
			Destroy(obj);
		}

		topBlock = null;
		topBlockTriggered = false;
	}
}
