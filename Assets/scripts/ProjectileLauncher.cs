using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour {
	int t;
	[SerializeField]
	int limit;
	bool paused;
	private void Start() {
		t = 0;
	}
	private void Update() {
		if(!paused){
			if (t >= limit){
				t = 0;
				GameObject current = Instantiate(Resources.Load("projectile")) as GameObject;
				current.transform.parent = transform;
				current.transform.localPosition = Vector3.zero;
			}
			else{
				t ++;
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
