using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointScript : MonoBehaviour {
	[SerializeField]
	Transform next;

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Enemy")){
			other.GetComponent<EnemyAI>().Goto(next);
		}
	}
}
