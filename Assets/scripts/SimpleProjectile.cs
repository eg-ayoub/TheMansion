using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProjectile : MonoBehaviour {

	public Vector2 speed, initialSpeed;
	Vector2 deltaPosition;
	[SerializeField]
	int gravity;
	int t; 
	[SerializeField]
	int deathTime;
	[SerializeField]
	int damage;

	bool paused;
	void Start () {
		speed = initialSpeed;
		t = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(!paused){
			if(t > deathTime){
				Destroy(gameObject);
			}
			speed -= gravity * Time.fixedDeltaTime * Vector2.up;
			deltaPosition = speed * Time.fixedDeltaTime;
			transform.Translate(deltaPosition);
			t ++;
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(!paused){
			if(other.CompareTag("PlayerHP")){
				other.SendMessage("TakeDamage", damage);
				Destroy(gameObject);
			}
			if(other.CompareTag("env")){
				Destroy(gameObject);
			}
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
}
