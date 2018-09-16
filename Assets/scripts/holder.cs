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
	
	void Update () {
		buttonDown = Input.GetButtonDown("back");
		buttonUp = Input.GetButtonUp("back");

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
						PlayerInstanciationScript.Player.GetComponentInChildren<PlayerHealthManager>().SendMessage("TakeDamage", 1);
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
}
