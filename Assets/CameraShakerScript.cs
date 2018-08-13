using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakerScript : MonoBehaviour {

	
	
	public static CameraShakerScript cam;
	private void Awake()
    {
        if (cam == null)
        {
            DontDestroyOnLoad(gameObject);
            cam = this;
        }
        else if (cam != this)
        {
            cam.transform.position = gameObject.transform.position;
            Destroy(gameObject);
        }
    }


	Animator anim;
	private void Start() {
		anim = GetComponent<Animator>();
	}

	public void VerticalShake(){
        GetComponent<AudioSource>().Play();
		anim.SetTrigger("vertical");
	}
	public void RandomShake(){
		anim.SetTrigger("random");
	}
}
