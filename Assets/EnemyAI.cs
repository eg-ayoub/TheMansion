using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	bool paused;
	[SerializeField]
	Transform wayPoint;

	public Vector2 direction;

	EnemyModifier modifier;
	bool done;
	private void Start() {
		modifier = GetComponent<EnemyModifier>();
		done = false;
	}

	void Update(){
		if(!paused){
			direction = Vector2.zero;
			if(wayPoint != null){
				if(Mathf.Abs(transform.position.x - wayPoint.position.x) > 1){
					direction.x = - Mathf.Sign(transform.position.x - wayPoint.position.x);
					if(direction.x > 0){
						transform.localScale = new Vector3(-1, 1, 1);
					}else{
						transform.localScale = new Vector3(1, 1, 1);
					}
				}
			}
			modifier.SetDirection(direction);
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

	public void Goto(Transform NewWayPoint){
		wayPoint = NewWayPoint;

	}
	
	private void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Player") && !done){
			PlayerInstanciationScript.Player.GetComponentInChildren<PlayerHealthManager>().SendMessage("TakeDamage", 1);
			done = true;
		}	
	}
}
