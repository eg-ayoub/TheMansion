using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatScript : MonoBehaviour {

	private void OnTriggerExit2D(Collider2D other) {
		if(other.CompareTag("Player") || other.CompareTag("Projectile") || other.CompareTag("Enemy")){
			StartCoroutine("FadeAway");
		}
	}


	IEnumerator FadeAway(){
		SpriteRenderer sprite = GetComponent<SpriteRenderer>();
		Color tmp;
		GetComponent<BoxCollider2D>().enabled = false;
		for(int i = 0; i < 20; i++){
			tmp = sprite.color;
			tmp.a = 1 - ((float)i)/20;
			sprite.color = tmp;
			yield return null;
		}
		Destroy(gameObject);
	}
}
