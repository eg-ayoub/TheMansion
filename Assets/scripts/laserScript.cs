using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserScript : MonoBehaviour {
	[SerializeField]
	GameObject beam;
	bool paused;
	[SerializeField]
	int delay, offtime, ontime;
	Animator anim;
	private void Start() {
		anim = GetComponent<Animator>();
		StartCoroutine("StartLaser");
	}

	IEnumerator StartLaser(){
		if(delay <= 20){
			anim.SetTrigger("telegraph");
		}
		for(int i =0; i < delay; i++){
			if(paused){
				i--;
			}
			else if(delay - i == 20){
				anim.SetTrigger("telegraph");
			}
			yield return null;
		}
		GetComponent<AudioSource>().Play();
		beam.SetActive(true);
		StartCoroutine("On");
	}
	IEnumerator Off(){
		for(int i = 0; i < offtime; i++){
			if(paused){
				i--;
			}
			if(i == offtime-20){
				anim.SetTrigger("telegraph");
			}
			yield return null;
		}
		GetComponent<AudioSource>().Play();
		beam.SetActive(true);
		StartCoroutine("On");
	}
	IEnumerator On(){
		for(int i = 0; i < ontime; i++){
			if(paused){
				i--;
			}
			yield return null;
		}
		GetComponent<AudioSource>().Pause();
		beam.SetActive(false);
		StartCoroutine("Off");
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
