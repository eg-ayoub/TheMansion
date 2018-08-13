using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineScript : MonoBehaviour {
	bool paused;
    Animator anim;
    private void Start() {
        anim = GetComponent<Animator>();
    }
	private void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Player") && !paused){
            GetComponent<AudioSource>().Play();
            anim.SetTrigger("bump");
            PlayerInstanciationScript.Player.GetComponentInChildren<Animator>().SetTrigger("jump");
			PlayerInstanciationScript.Player.GetComponentInChildren<PlayerMovementModifier>().SetSpeed(new Vector2(0, 2000));
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

