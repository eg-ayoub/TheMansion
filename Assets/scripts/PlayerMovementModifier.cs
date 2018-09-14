using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementModifier : MonoBehaviour {

    //physics actors
    Vector2 groundVector;
    [SerializeField]
    Vector2 Speed;
    PlayerPhysics PlayerPhysics;
    CollisionSensorScript sensor;
    //managed by sensor
    bool PlayerOnRightWall, PlayerOnLeftWall, PlayerOnGround, PlayerOnCeiling;
    //managed by controller
    bool JumpButtonDown, JumpButtonUp;
    float horizontal, vertical;
    //states
    bool paused;
    //double jump limiter
    bool canDoubleJump;
    //physics variables
    public float maxRunSpeed;
    float maxAirborneSpeed;
    [HideInInspector]
    public float VerticalJumpSpeed;
    public float JumpHeight;
    public float JumpTime;
    public float maxJumpWidth;
    [Range(0, 1)]
    public float slideParameter;
    [Range(0, 1)]
    public float minSlopeSlideX;
    public float slopeSlideSpeed;
    public float ClimbSpeed;
    // public float SwimSpeed;
    // public float dash_multiplier;
    // public float dash_time;


    int shakecounter;

    bool swimming;
    bool onSurface;


    Animator anim;
	void Start () {
        sensor = GetComponentInChildren<CollisionSensorScript>();
        PlayerPhysics = GetComponent<PlayerPhysics>();

        groundVector = Vector2.right;
        PlayerPhysics.gravity = (8 * JumpHeight) / (JumpTime * JumpTime);
        maxAirborneSpeed = maxJumpWidth / JumpTime;
        VerticalJumpSpeed = (4 * JumpHeight) / JumpTime;

        paused = false;

        shakecounter = 0;
        anim = PlayerInstanciationScript.Player.GetComponentInChildren<Animator>();

	}

	public void SetSpeed(Vector2 TS) {
        Speed = TS;
    }



    public void SetControls(bool JUP, bool JDOWN, float h, float v)
    {
        JumpButtonUp = JUP;
        JumpButtonDown = JDOWN;
        horizontal = h;
        vertical = v;
    }

    
    

    private void Update()
    {
        
        if (!paused)
        {
            if(!swimming)
            {
                PlayerOnGround = sensor.info.down;
                PlayerOnCeiling = sensor.info.up;
                PlayerOnRightWall = sensor.info.right;
                PlayerOnLeftWall = sensor.info.left;
                groundVector = sensor.info.groundVector;


                if(PlayerOnGround){
                    anim.SetTrigger("land");
                    if(shakecounter > 30){
				        CameraShakerScript.cam.VerticalShake();
                    }
                    shakecounter = 0;
                }else{
                    shakecounter++;
                }

                if(PlayerOnRightWall || PlayerOnLeftWall){
                    anim.SetTrigger("hitwall");
                }

                if (Mathf.Abs(horizontal) <= 10E-2)
                {
                    if (PlayerOnGround)
                    {
                        GetComponent<AudioSource>().enabled = false;
                        Speed.x *= slideParameter;
                        Speed.y = 0;
                        if (sensor.info.groundVector.x < minSlopeSlideX)
                        {
                            Speed = -slopeSlideSpeed * Mathf.Sign(sensor.info.groundVector.y) * groundVector;
                        }
                        canDoubleJump = true;
                        anim.SetFloat("runBlend", Mathf.Abs(horizontal));

                    }
                    
                }
                else if (!PlayerOnLeftWall && !PlayerOnRightWall)
                {
                    if (PlayerOnGround)
                    {
                        GetComponent<AudioSource>().enabled = true;
                        Speed = horizontal * maxRunSpeed * groundVector;
                        anim.SetFloat("runBlend", Mathf.Abs(horizontal));
                        canDoubleJump = true;
                    }
                    else
                    {
                        Speed.x = horizontal * maxAirborneSpeed;
                    }
                }
                else if (PlayerOnGround)
                {
                    canDoubleJump = true;
                    Speed = horizontal * maxRunSpeed * groundVector;
                }
                else
                {
                    Speed.y *= .5f;
                }

                if (JumpButtonDown)
                {

                    if (PlayerOnGround)
                    {
                        anim.SetTrigger("jump");
                        GetComponent<AudioSource>().enabled = false;
                        Speed.y = VerticalJumpSpeed;
                    }
                    else if (PlayerOnLeftWall || PlayerOnRightWall)
                    {
                            Speed.y = ClimbSpeed;
                            Speed.x = (PlayerOnLeftWall ? 1 : -1) * maxAirborneSpeed;
                    }
                    else if (canDoubleJump)
                    {
                        anim.SetTrigger("jump");
                        GetComponent<AudioSource>().enabled = false;
                        Speed.y = VerticalJumpSpeed;
                        canDoubleJump = false;
                    }

                }
                



                if(Speed.y < 0){
                    anim.SetTrigger("fall");
                }
                if (JumpButtonUp && Speed.y > 0 && !PlayerOnGround)
                {
                    Speed.y = 0;
                }
                PlayerPhysics.SetTargetSpeed(Speed);
                ResetStats();
            }
            else
            {/* 
                if(onSurface){
                    if(JumpButtonDown && vertical < -0.5f){
                        onSurface = false;
                    }else if(JumpButtonDown && vertical > 0.5f){
                        JumpOut();
                    }else{
                        if(Mathf.Abs(horizontal) > 10E-2){
                            Speed = horizontal * SwimSpeed * Vector2.right;
                        }
                    }
                }else{
                    if(JumpButtonDown){
                        Dash();
                        Invoke("StopDash", dash_time);
                    }
                    if(Mathf.Abs(horizontal) > 10E-2){
                        Speed.x = horizontal  * SwimSpeed;
                    }
                    if(Mathf.Abs(vertical) > 10E-2){
                        Speed.y = vertical * SwimSpeed;
                    }
                }
                PlayerPhysics.SetTargetSpeed(Speed);
                horizontal = 0;
                vertical = 0;*/
            }
        }
    }

    private void ResetStats()
    {
        PlayerOnCeiling = false;
        PlayerOnGround = false;
        PlayerOnRightWall = false;
        PlayerOnLeftWall = false;
        horizontal = 0;
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
