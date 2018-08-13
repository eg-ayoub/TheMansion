using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDStay : MonoBehaviour {


	public static HUDStay HUD;
	private void Awake()
    {
        if (HUD == null)
        {
            DontDestroyOnLoad(gameObject);
            HUD = this;
        }
        else if (HUD != this)
        {
            HUD.transform.position = gameObject.transform.position;
            Destroy(gameObject);
        }
    }
	
}
