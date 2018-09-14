using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineScript : MonoBehaviour {
	bool paused;
    Animator anim;
    private void Start() {
        anim = GetComponentInChildren<Animator>();
    }
	private void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Player") && !paused){
            GetComponent<AudioSource>().Play();
            anim.SetTrigger("bump");
            PlayerInstanciationScript.Player.transform.Translate(100 * Vector2.up);
            PlayerInstanciationScript.Player.GetComponentInChildren<Animator>().SetTrigger("jump");
			PlayerInstanciationScript.Player.GetComponentInChildren<PlayerMovementModifier>().SetSpeed(new Vector2(0, 2000));
            //TODO: maybe add coroutine to reset ( avoid player sticking ? ) 
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

