using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holder : MonoBehaviour {

	Animator anim;
	enum states {IDLE, COUNT, RESET};
	states state;
	bool buttonUp, buttonDown;
	int counter;

	void Start () {
		anim = GetComponent<Animator>();
		state = states.IDLE;
		counter = 0;
	}
	
	bool paused;
	void Update () {
		if(paused){
			buttonDown = Input.GetButtonDown("quit");
			buttonUp = Input.GetButtonUp("quit");
		}else{
			buttonDown = Input.GetButtonDown("back");
			buttonUp = Input.GetButtonUp("back");
		}
		
		switch(state){
			case states.IDLE:
				if(buttonDown){
					counter = 0;
					state = states.COUNT;
					anim.SetFloat("speed", 1);
				}
				break;

			case states.COUNT:
				if(buttonUp){
					anim.SetFloat("speed", -1);
					state = states.RESET;
				}else{
					counter++;
					if(counter == 120){
						state = states.IDLE;
						anim.Play("load", -1, 0f);
						anim.SetFloat("speed", 0);
						if(paused){
							Application.Quit();
						}else{
							PlayerInstanciationScript.Player.GetComponentInChildren<PlayerHealthManager>().SendMessage("TakeDamage", 1);
						}
					}
				}
				break;

			case states.RESET:
				if(buttonDown){
					anim.SetFloat("speed", 1);
					state = states.COUNT;
				}else{
					counter--;
					if(counter == 0){
						state = states.IDLE;
						anim.Play("load", -1, 0f);
						anim.SetFloat("speed", 0);
					}
				}
				break;

		}
	}

	public void OnPauseGame()
    {
        paused = true;
		state = states.IDLE;
		counter = 0;

    }

    public void OnResumeGame()
    {
        paused = false;
		state = states.IDLE;
		counter = 0;
    }
}
