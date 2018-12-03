using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour {

	public bool done;
	private void Start() {
		done = false;
	}
	private void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Player") && ! done){
			PlayerInstanciationScript.Player.GetComponentInChildren<PlayerHealthManager>().SendMessage("TakeDamage", 1);
			done = true;
		}
	}
}
