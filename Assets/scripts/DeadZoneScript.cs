using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneScript : MonoBehaviour {

	bool givingDamage;
	bool paused;
	private void Start() {
		givingDamage = true;
	}
	private void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Player") && !paused){
			if(givingDamage){
				givingDamage = false;
				PlayerInstanciationScript.Player.GetComponentInChildren<PlayerHealthManager>().SendMessage("TakeDamage", 1);
			}
		}
	}

	public void OnPauseGame()
    {
        paused = true;
    }

    public void OnResumeGame()
    {
        paused = false;
    }
}
