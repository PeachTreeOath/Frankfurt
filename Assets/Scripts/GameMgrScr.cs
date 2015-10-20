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
	private Text summonText;
	private CraneScr crane;
	private Camera cam;
	private int playerHP = 50;
	private int enemyHP = 50;
	private Text playerHPText;
	private Text enemyHPText;
	private Text dmgText;
	private float lastEnemySummon;
	private float nextSummon;
	private WinScr winTracker;
	private int enemySummonCount;
	public List<string> summonNames;
	public List<int> summonHPs;
	public List<GameObject> summonPFs;
	public GameObject summon1;
	public GameObject summon2;
	public GameObject summon3;
	public GameObject summon4;
	public GameObject summon5;
	public GameObject summon6;
	public GameObject summon7;
	public GameObject summon8;
	public GameObject summon9;
	public GameObject summon10;
	public GameObject summon11;
	public GameObject summon12;
	public float summonPointX;
	public float summonPointY;
	public float enemySummonPointX;
	public float enemySummonPointY;
	public float summonRangeMinTime;
	public float summonRangeMaxTime;

	// Use this for initialization
	void Start ()
	{
		summonParent = GameObject.Find ("Summons");		
		summonButton = GameObject.Find ("SummonButton").GetComponent<Button> ();
		crane = GameObject.Find ("Crane").GetComponent<CraneScr> ();
		cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		summonText = GameObject.Find ("SummonText").GetComponent<Text> ();
		summonText.color = new Color (summonText.color.r, summonText.color.g, summonText.color.b, 0);
		lastEnemySummon = Time.time;
		nextSummon = Random.Range (summonRangeMinTime * 2, summonRangeMaxTime * 2);
		playerHPText = GameObject.Find ("PlayerHP").GetComponent<Text> ();
		enemyHPText = GameObject.Find ("EnemyHP").GetComponent<Text> ();
		dmgText = GameObject.Find ("DmgText").GetComponent<Text> ();
		dmgText.color = new Color (dmgText.color.r, dmgText.color.g, dmgText.color.b, 0);
		winTracker = GameObject.Find ("WinTracker").GetComponent<WinScr> ();

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
		summonPFs.Add (summon10);
		summonPFs.Add (summon11);
		summonPFs.Add (summon12);

		summonNames.Add ("KAWAEI");
		summonNames.Add ("SNOWMAN");
		summonNames.Add ("APOLLO");
		summonNames.Add ("YMIR");
		summonNames.Add ("MOTHRA");
		summonNames.Add ("ROBOT");
		summonNames.Add ("MINOTAUR");
		summonNames.Add ("FIRE BEAR");
		summonNames.Add ("GENIE");
		summonNames.Add ("TWIN DRAGON");
		summonNames.Add ("GREAT APE");
		summonNames.Add ("EXODIA");

		summonHPs.Add (1);
		summonHPs.Add (2);
		summonHPs.Add (3);
		summonHPs.Add (4);
		summonHPs.Add (6);
		summonHPs.Add (8);
		summonHPs.Add (10);
		summonHPs.Add (12);
		summonHPs.Add (15);
		summonHPs.Add (18);
		summonHPs.Add (23);
		summonHPs.Add (50);
	}

	// Update is called once per frame
	void Update ()
	{
		if (GameObject.FindGameObjectWithTag ("Dropping")) {
			summonButton.interactable = false;
		} else if (stackHeight > 0) {
			summonButton.interactable = true;
			crane.GenerateChild ();
			crane.SetSpeed (stackHeight);
		}

		if (Time.time > lastEnemySummon + nextSummon) {
			EnemySummon ();
			lastEnemySummon = Time.time;
			nextSummon = Random.Range (summonRangeMinTime, summonRangeMaxTime);
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
		if (stackHeight > 12) {
			stackHeight = 12;
		}
		GameObject summonPF = summonPFs [stackHeight - 1];
		GameObject newSummon = (GameObject)GameObject.Instantiate (summonPF, new Vector3 (summonPointX + summonPF.GetComponent<SpriteRenderer> ().bounds.size.x / 2, summonPointY + summonPF.GetComponent<SpriteRenderer> ().bounds.size.y / 2, 0), Quaternion.identity);
		newSummon.transform.parent = summonParent.transform;
		SummonScr scr = newSummon.GetComponent<SummonScr> ();
		scr.isPlayer = true;
		scr.HP = summonHPs [stackHeight - 1];

		FadeText (stackHeight);
		PlayBlocks ();
		DisableCrane ();
	}

	private void EnemySummon ()
	{
		int level;
		if (enemySummonCount < 5) {
			level = Random.Range (0, 5);
		} else if (enemySummonCount < 10) {
			level = Random.Range (2, 9);
		} else {
			level = Random.Range (4, 11);
		}
		enemySummonCount++;

		GameObject summonPF = summonPFs [level];
		GameObject newSummon = (GameObject)GameObject.Instantiate (summonPF, new Vector3 (enemySummonPointX - summonPF.GetComponent<SpriteRenderer> ().bounds.size.x / 2, enemySummonPointY + summonPF.GetComponent<SpriteRenderer> ().bounds.size.y / 2, 0), Quaternion.identity);
		newSummon.transform.parent = summonParent.transform;
		newSummon.transform.localRotation = Quaternion.Euler (0, 180, 0);
		SummonScr newSummonScr = newSummon.GetComponent<SummonScr> ();
		newSummonScr.moveSpeed = -newSummonScr.moveSpeed;
		newSummonScr.HP = summonHPs [level];
	}

	public void PlayerTakeDmg (int dmg)
	{
		playerHP -= dmg;
		if (playerHP < 0) {
			winTracker.win = false;
			Application.LoadLevel ("GameOver");
		}

		playerHPText.text = "Player HP: " + playerHP;
		dmgText.text = "You took " + dmg + " damage!";
		dmgText.color = new Color (dmgText.color.r, dmgText.color.g, dmgText.color.b, 1);
		Invoke ("HideDmg", 2f);
	}

	public void EnemyTakeDmg (int dmg)
	{
		enemyHP -= dmg;
		if (enemyHP < 0) {
			winTracker.win = true;
			Application.LoadLevel ("GameOver");
		}

		enemyHPText.text = "Enemy HP: " + enemyHP;
		dmgText.text = "You dealt " + dmg + " damage!";
		dmgText.color = new Color (dmgText.color.r, dmgText.color.g, dmgText.color.b, 1);
		Invoke ("HideDmg", 2f);
	}

	private void HideDmg ()
	{
		dmgText.color = new Color (dmgText.color.r, dmgText.color.g, dmgText.color.b, 0);
	}

	private void FadeText (int currStackHeight)
	{
		currStackHeight--;
		if (currStackHeight > 11) {
			currStackHeight = 11;
		}

		string newText = "Monster #" + (currStackHeight + 1) + ": ";
		string monsterName = summonNames [currStackHeight];
		int monsterHP = summonHPs [currStackHeight];
		newText += monsterName + " (" + monsterHP + " HP)";

		summonText.text = newText;
		StartCoroutine ("FadeInCR");
		Invoke ("FadeOutCaller", 4f);
	}
	
	private IEnumerator FadeInCR ()
	{
		float duration = 0.5f;
		float currentTime = 0f;
		while (currentTime < duration) {
			float alpha = Mathf.Lerp (0f, 1f, currentTime / duration);
			summonText.color = new Color (summonText.color.r, summonText.color.g, summonText.color.b, alpha);
			currentTime += Time.deltaTime;
			yield return null;
		}
		summonText.color = new Color (summonText.color.r, summonText.color.g, summonText.color.b, 1);
		yield break;
	}

	private void FadeOutCaller ()
	{
		StartCoroutine ("FadeOutCR");
	}

	private IEnumerator FadeOutCR ()
	{
		float duration = 0.5f;
		float currentTime = 0f;
		while (currentTime < duration) {
			float alpha = Mathf.Lerp (1f, 0f, currentTime / duration);
			summonText.color = new Color (summonText.color.r, summonText.color.g, summonText.color.b, alpha);
			currentTime += Time.deltaTime;
			yield return null;
		}
		summonText.color = new Color (summonText.color.r, summonText.color.g, summonText.color.b, 0);
		yield break;
	}
	
	// Set the top block
	private void SetTopBlock (GameObject block)
	{
		topBlock = block;
		topBlockTriggered = false;
		stackHeight++;
		crane.SetSpeed (stackHeight);
		summonButton.interactable = true;
		if (stackHeight >= 12) {
			summonButton.GetComponentInChildren<Text> ().text = "SUMMON LVL MAX";
		} else {
			summonButton.GetComponentInChildren<Text> ().text = "SUMMON LVL " + stackHeight;
		}
		cam.transform.position = new Vector3 (cam.transform.position.x, cam.transform.position.y + block.GetComponent<SpriteRenderer> ().bounds.size.y, cam.transform.position.z);
		crane.transform.position = new Vector3 (crane.transform.position.x, crane.transform.position.y + block.GetComponent<SpriteRenderer> ().bounds.size.y, crane.transform.position.z);
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
		crane.SetSpeed (stackHeight);
		summonButton.interactable = false;
		summonButton.GetComponentInChildren<Text> ().text = "SUMMON";

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
