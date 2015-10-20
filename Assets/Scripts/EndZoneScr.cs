using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndZoneScr : MonoBehaviour {

	private GameMgrScr mgr;
	
	void Start ()
	{
		mgr = GameObject.Find ("GameMgr").GetComponent<GameMgrScr> ();
	}

	void OnTriggerEnter2D (Collider2D collider)
	{
		SummonScr summon = collider.gameObject.GetComponent<SummonScr> ();
		if (summon == null) {
			return;
		}

		if (summon.isPlayer) {
			mgr.EnemyTakeDmg (summon.HP);
		} else {
			mgr.PlayerTakeDmg (summon.HP);
		}

		Destroy (collider.gameObject);
	}
}
