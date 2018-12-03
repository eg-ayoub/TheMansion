using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorComplement : MonoBehaviour {

	Vector3 tmp;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		tmp = transform.localScale;
		switch(PlayerInstanciationScript.Player.direction){
			case(PlayerInstanciationScript.DIRECTION.RIGHT):
				tmp.x = Mathf.Abs(tmp.x);
				break;
			case(PlayerInstanciationScript.DIRECTION.LEFT):
				tmp.x = - Mathf.Abs(tmp.x);
				break;
			
		}
		transform.localScale = tmp;
	}
}
