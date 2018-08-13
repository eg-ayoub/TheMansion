using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModifier : MonoBehaviour {
	bool paused;
	Vector2 direction;
	Vector2 speed;
	EnemyPhysics physics;
	[SerializeField]
	float runSpeed;
	void Start () {
		direction = speed = Vector2.zero;
		physics = GetComponent<EnemyPhysics>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!paused){
			if(direction.x != 0){
				speed.x = runSpeed * direction.x;
			}


			physics.SetSpeed(speed);
		}
	}

	internal void SetSpeed(Vector2 Speed){
		speed = Speed;
	}
    internal void SetDirection(Vector2 Direction)
    {
        direction = Direction;
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
