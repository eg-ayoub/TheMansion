using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creditsScript : MonoBehaviour {

	
	void Start () {
		StartCoroutine("QuitGame");
	}
	IEnumerator QuitGame(){
		for(int i = 0; i < 1080; i++){
			yield return null;
		}
		Application.Quit();
	}
}
