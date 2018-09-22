using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineScript : MonoBehaviour {
	bool paused;
    Animator anim;
    bool ready;
    private void Start() {
        anim = GetComponentInChildren<Animator>();
        ready = true;
    }
	private void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Player") && !paused && ready){
            GetComponent<AudioSource>().Play();
            anim.SetTrigger("bump");
            PlayerInstanciationScript.Player.transform.Translate(100 * Vector2.up);
            PlayerInstanciationScript.Player.GetComponentInChildren<Animator>().SetTrigger("jump");
			PlayerInstanciationScript.Player.GetComponentInChildren<PlayerMovementModifier>().SetSpeed(new Vector2(0, 2000));
            StartCoroutine("Reset");
		}
	}

    IEnumerator Reset(){
        ready = false;
        for(int i = 0; i < 10; i++){
            yield return null;
        }
        ready = true;
        yield return null;
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

