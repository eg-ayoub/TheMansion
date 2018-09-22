using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class level_manager : MonoBehaviour {

	int level;
	PlayerHealthManager playerHealth;
	bool key_taken;
	bool done;
	[SerializeField]
	GameObject transitionText;

	private void Start() {
		level =SceneManager.GetActiveScene().buildIndex;
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
			transitionText.GetComponentInChildren<Text>().text = (level + 1 == 31 ? "Credits" : "" + (level + 1));
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
		PlayerInstanciationScript.Player.GetComponentInChildren<PlayerMovementModifier>().SetSpeed(Vector2.zero);
		SceneManager.LoadSceneAsync(level + 1);	
	}
}
