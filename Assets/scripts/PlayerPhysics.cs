using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour {

    //paused
    bool paused;

    Vector2 targetSpeed;
    public float gravity;
    public float gravity_swimming;
    //Vector2 speed;
    Vector2 deltaPosition;

    [SerializeField]
    float dampeningFactor;


    //speed limits
    public int maxSpeedX;
    public int maxSpeedY;
    public int maxSpeedSwimmingX;
    public int maxSpeedSwimmingY;

    
    bool swimming;
    
    PlayerMovementModifier MovementModifier;
    CollisionSensorScript sensor;
    PlayerClipManager ClipManager;


	
	void Start () {
        MovementModifier = GetComponentInParent<PlayerMovementModifier>();
        sensor = GetComponentInChildren<CollisionSensorScript>();
        ClipManager = GetComponent<PlayerClipManager>();
        paused = false;
	}

    private void FixedUpdate()
    {
        if (!paused)
        {
            if(!swimming)
            {
                if (!sensor.info.down)
                {
                    targetSpeed -= gravity * Time.fixedDeltaTime * Vector2.up;
                }
                ClampSpeed(maxSpeedX, maxSpeedY);
            }
            else
            {
                if(Mathf.Abs(Input.GetAxis("Vertical")) < 10E-2 && Mathf.Abs(Input.GetAxis("Horizontal")) < 10E-2){
                    targetSpeed -= dampeningFactor * targetSpeed;
                    targetSpeed -= Time.deltaTime * gravity_swimming * Vector2.up;
                }
                ClampSpeed(maxSpeedSwimmingX, maxSpeedSwimmingY);
            }

            MovementModifier.SetSpeed(targetSpeed);
            deltaPosition = targetSpeed * Time.fixedDeltaTime;
            ClipManager.SetDeltaPosition(deltaPosition);
        }

    }

    private void ClampSpeed(float max_x, float max_y)
    {
        targetSpeed.x = Mathf.Sign(targetSpeed.x) * Mathf.Clamp(Mathf.Abs(targetSpeed.x), 0, max_x);
        targetSpeed.y = Mathf.Sign(targetSpeed.y) * Mathf.Clamp(Mathf.Abs(targetSpeed.y), 0, max_y);
    }



    public void SetTargetSpeed(Vector2 speed)
    {
        targetSpeed = speed;
    }

    public void OnPauseGame()
    {
        paused = true;
    }

    public void OnResumeGame()
    {
        paused = false;
    }
    public void Freeze()
    {
        targetSpeed = Vector2.zero;
    }

    // public void Dive(){
    //     swimming = true;
    //     ClipManager.Dive();
    // }
    // public void JumpOut(){
    //     if(swimming){
    //         swimming = false;
    //         ClipManager.JumpOut();
    //     }
    // }
}
