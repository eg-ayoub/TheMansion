using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyScript : MonoBehaviour {

	bool paused;
	[SerializeField]
	level_manager levelManager;

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Player") && !paused){
			levelManager.TakeKey();
			StartCoroutine("Disable");
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

	IEnumerator Disable(){
		GameObject current = Instantiate(Resources.Load("particleflash")) as GameObject;
		current.transform.parent = transform;
		current.transform.localPosition = Vector3.zero;
		GetComponent<AudioSource>().Play();
		GetComponent<CircleCollider2D>().enabled = false;
		GetComponentInChildren<SpriteRenderer>().enabled = false;
		for(int i=0; i< 60; i++){
			yield return null;
		}
		Destroy(gameObject);
	}
}
