using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorWayScript : MonoBehaviour {

	[SerializeField]
	level_manager levelManager;
	bool paused;
	private void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Player") && !paused){
			levelManager.EnterDoor();
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
