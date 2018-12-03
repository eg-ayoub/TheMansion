using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPhysics : MonoBehaviour {

	bool paused;
	Vector2 Speed;
	[SerializeField]
	float gravity;
	Vector2 deltaPosition;
	EnemyClipManager clipManager;
	EnemyModifier modifier;
	[SerializeField]
	float maxSpeedX, maxSpeedY;
	void Start () {
		clipManager = GetComponent<EnemyClipManager>();
		modifier = GetComponent<EnemyModifier>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(!paused){
			Speed -= gravity * Time.fixedDeltaTime * Vector2.up;

			Speed.x = Mathf.Sign(Speed.x) * Mathf.Clamp(Mathf.Abs(Speed.x), 0, maxSpeedX);
			Speed.y = Mathf.Sign(Speed.y) * Mathf.Clamp(Mathf.Abs(Speed.y), 0, maxSpeedY);

			deltaPosition = Speed * Time.fixedDeltaTime;

			clipManager.SetDeltaPosition(deltaPosition);
			Speed.x = 0;
			modifier.SetSpeed(Speed);

		}
	}

    internal void SetSpeed(Vector2 speed)
    {
        Speed = speed;
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
