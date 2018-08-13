using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPointScript : MonoBehaviour {

	[SerializeField, Range(1, 10)]
	int Health;
	[SerializeField]
	bool permanent;
	bool on;
	[SerializeField]
	int restoreTime;
	private void Start() {
		on = true;
	}
	private void OnTriggerStay2D(Collider2D other) {
		if(other.CompareTag("Player")){
			if(on){
				on = false;
				PlayerInstanciationScript.Player.GetComponentInChildren<PlayerHealthManager>().SendMessage("AddMaxHP", Health);
				PlayerInstanciationScript.Player.GetComponentInChildren<PlayerHealthManager>().SendMessage("Heal", Health);
				if(!permanent){
					StartCoroutine("Disable");
				}else{
					StartCoroutine("Restore");
				}
			}
		}
	}

	IEnumerator Restore(){
		for(int i = 0; i < restoreTime; i++){
			yield return 0;
		}
		on = true;
	}

	IEnumerator Disable(){
		GetComponent<AudioSource>().Play();
		GetComponent<CircleCollider2D>().enabled = false;
		GetComponentInChildren<SpriteRenderer>().enabled = false;
		GetComponentInChildren<Light>().enabled = false;
		for(int i=0; i< 60; i++){
			yield return null;
		}
		Destroy(gameObject);
	}
}
