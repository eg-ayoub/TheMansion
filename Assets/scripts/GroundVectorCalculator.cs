using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundVectorCalculator : MonoBehaviour {

	bool paused;
    float angle1, angle2;
    float angle;
    int maxRaycastDepth;
    public LayerMask layerMask;
    CollisionSensorScript sensor;

	// Use this for initialization
	void Start () {
        sensor = GetComponentInParent<CollisionSensorScript>();
        paused = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!paused)
        {
            angle1 = angle2 = 0;
            Cast(ref angle1, true);
            Cast(ref angle2, false);
            angle = (Mathf.Abs(angle1) > Mathf.Abs(angle2) ? angle1 : angle2);
            sensor.info.groundVector = new Vector2(Mathf.Cos(Mathf.Deg2Rad*angle), Mathf.Sin(Mathf.Deg2Rad*angle));
            


        }
	}

    void Cast(ref float angle, bool dir){
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + new Vector2((dir ?-1 : 1) * 6, 5),-Vector2.up, 100, layerMask);
            if (hit)
            {
                angle = Vector2.SignedAngle(Vector2.up, hit.normal);
            }
    }

	public void OnPauseGame () {
		paused = true;
	
	}
	public void OnResumeGame () {
		paused = false;
	
	}
}
