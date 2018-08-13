using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAI : MonoBehaviour {

	bool paused;
	[SerializeField]
	float speed;

	bool done;
	private void Start() {
		done = false;
	}

	private void Update(){
		if(!paused){
			Vector2 move = ((Vector2)(PlayerInstanciationScript.Player.transform.position - transform.position)).normalized * Time.deltaTime * speed;
			if(move.x > 0){
				transform.localScale = new Vector3(-1, 1, 1);
			}else{
				transform.localScale = new Vector3(1, 1, 1);
			}
			transform.Translate(move);
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

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Player") && !done){
			PlayerInstanciationScript.Player.GetComponentInChildren<PlayerHealthManager>().SendMessage("TakeDamage", 1);
			done = true;
		}	
	}

}
