using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class level_manager : MonoBehaviour {

	[SerializeField]
	int level;
	PlayerHealthManager playerHealth;
	bool key_taken;
	bool done;
	[SerializeField]
	GameObject transitionText;

	private void Start() {
		playerHealth = PlayerInstanciationScript.Player.GetComponentInChildren<PlayerHealthManager>();
		PlayerInstanciationScript.Player.transform.position = transform.position;
		key_taken = false;
		done = false;
	}

	public void TakeKey(){
		key_taken = true;
	}

	public void EnterDoor(){
		if(key_taken && !done){
			GetComponent<AudioSource>().Play();
			playerHealth.IncrementScene();
			transitionText.SetActive(true);
			transitionText.GetComponentInChildren<Text>().text = "" + (level + 1);
			StartCoroutine("pass");
			done = true;
		}
	}

	IEnumerator pass(){
		playerHealth.ToggleGamePaused();
		for(int i =0; i < 60; i++){
			yield return null;
		}
		playerHealth.ToggleGamePaused();
		SceneManager.LoadSceneAsync(level + 1);	
	}
}
