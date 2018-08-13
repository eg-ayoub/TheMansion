using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorOnOff : MonoBehaviour {

    public bool on;
	void Start () {
        on = false;
	}
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("env")) {
            on = true;
        }

    }
    public void Reset()
    {
        on = false;
    }
}
