using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownPlatform : MonoBehaviour {

	[SerializeField]
	int limit;
	int t;
	bool paused;

	Animator anim;
	SpriteRenderer sprite;
	private void Start() {
		anim = GetComponent<Animator>();
		sprite = GetComponent<SpriteRenderer>();
		StartCoroutine("Manage");
	}

	private void OnTriggerStay2D(Collider2D other) {
		if(other.CompareTag("Player") && !paused){
			t++;
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

	IEnumerator Manage(){
		while(t < limit){
			if(t == 1){
				anim.SetTrigger("pass");
				CameraShakerScript.cam.RandomShake();
			}
			if(t == limit/3){
				anim.SetTrigger("pass");
				CameraShakerScript.cam.RandomShake();
			}
			if(t == 2*limit/3){
				anim.SetTrigger("pass");
				CameraShakerScript.cam.RandomShake();
			}
			yield return null;
		}
		anim.SetTrigger("pass");
		Color tmp; 
		GetComponent<BoxCollider2D>().enabled = false;
		for(int i = 0; i < 15; i++){
			tmp = sprite.color;
			tmp.a = 1 - ((float)i)/15;
			sprite.color = tmp;
			yield return null;
		}
		Destroy(gameObject);

	}
}
