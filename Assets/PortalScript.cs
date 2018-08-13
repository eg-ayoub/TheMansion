using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour {
	[SerializeField]
	GameObject to;

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Player")){
			PlayerInstanciationScript.Player.transform.position = to.transform.position;
		}
	}

}
